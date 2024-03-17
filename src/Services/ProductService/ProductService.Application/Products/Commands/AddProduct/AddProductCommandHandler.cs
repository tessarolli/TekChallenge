// <copyright file="AddProductCommandHandler.cs" company="Tek">
// Copyright (c) TekChallenge.Services.ProductService. All rights reserved.
// </copyright>

using FluentResults;
using System.Diagnostics;
using TekChallenge.Services.ProductService.Application.Abstractions.Repositories;
using TekChallenge.Services.ProductService.Application.Products.Dtos;
using TekChallenge.Services.ProductService.Domain.Products;
using TekChallenge.SharedDefinitions.Application.Abstractions.Messaging;
using TekChallenge.SharedDefinitions.Application.Common.Errors;

namespace TekChallenge.Services.ProductService.Application.Products.Commands.AddProduct;

/// <summary>
/// Mediator Handler for the <see cref="AddProductCommand"/>.
/// </summary>
public class AddProductCommandHandler : ICommandHandler<AddProductCommand, ProductDto>
{
    private readonly IProductRepository _productRepository;

    /// <summary>
    /// Initializes a new instance of the <see cref="AddProductCommandHandler"/> class.
    /// </summary>
    /// <param name="ProductRepository">Injected UserRepository.</param>
    public AddProductCommandHandler(IProductRepository ProductRepository)
    {
        _productRepository = ProductRepository;
    }

    /// <inheritdoc/>
    public async Task<Result<ProductDto>> Handle(AddProductCommand request, CancellationToken cancellationToken)
    {
        var productDomainModel = Product.Create(
                   null,
                   request.Name,
                   request.Description,
                   0,
                   request.BasePrice,
                   ownerId: request.OwnerId);

        if (!productDomainModel.IsSuccess)
        {
            return Result.Fail(productDomainModel.Errors);
        }

        var addResult = await _productRepository.AddAsync(productDomainModel.Value);
        if (!addResult.IsSuccess)
        {
            return Result.Fail(addResult.Errors);
        }

        var discountTask = addResult.Value.Discount?.Value;
        if (discountTask is null)
        {
            return Result.Fail(new UnreachableExternalServiceError("Discount Service"));
        }

        return Result.Ok(new ProductDto(
            addResult.Value.Id.Value,
            addResult.Value.OwnerId,
            addResult.Value.Name,
            addResult.Value.Description,
            addResult.Value.StatusName,
            addResult.Value.Stock,
            addResult.Value.Price,
            (await discountTask)?.Amount ?? 0,
            await addResult.Value.FinalPrice(),
            addResult.Value.CreatedAtUtc));
    }
}
