// <copyright file="UpdateProductCommandHandler.cs" company="Tek">
// Copyright (c) TekChallenge.Services.ProductService. All rights reserved.
// </copyright>

using FluentResults;
using TekChallenge.Services.ProductService.Application.Abstractions.Repositories;
using TekChallenge.Services.ProductService.Application.Products.Dtos;
using TekChallenge.Services.ProductService.Domain.Dtos;
using TekChallenge.Services.ProductService.Domain.Products;
using TekChallenge.Services.ProductService.Domain.Products.ValueObjects;
using TekChallenge.SharedDefinitions.Application.Abstractions.Messaging;
using TekChallenge.SharedDefinitions.Application.Common.Errors;

namespace TekChallenge.Services.ProductService.Application.Products.Commands.UpdateProduct;

/// <summary>
/// Mediator Handler for the <see cref="UpdateProductCommand"/>.
/// </summary>
public class UpdateProductCommandHandler : ICommandHandler<UpdateProductCommand, ProductDto>
{
    private readonly IProductRepository _productRepository;

    /// <summary>
    /// Initializes a new instance of the <see cref="UpdateProductCommandHandler"/> class.
    /// </summary>
    /// <param name="ProductRepository">Injected UserRepository.</param>
    public UpdateProductCommandHandler(IProductRepository ProductRepository)
    {
        _productRepository = ProductRepository;
    }

    /// <inheritdoc/>
    public async Task<Result<ProductDto>> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
    {
        var productDomainModel = Product.Create(
            new ProductId(request.Id),
            request.Name,
            request.Description,
            request.Stock,
            request.BasePrice,
            ownerId: request.OwnerId);

        if (!productDomainModel.IsSuccess)
        {
            return Result.Fail(productDomainModel.Errors);
        }

        var updateResult = await _productRepository.UpdateAsync(productDomainModel.Value);
        if (!updateResult.IsSuccess)
        {
            return Result.Fail(updateResult.Errors);
        }

        if (updateResult.Value.Discount is null)
        {
            return Result.Fail(new Error("Internal Server Error: Can't get product's discount."));
        }

        var discount = await updateResult.Value.Discount.Value;
        if (discount is null)
        {
            return Result.Fail(new UnreachableExternalServiceError("Discount Service"));
        }

        return Result.Ok(new ProductDto(
            updateResult.Value.Id.Value,
            updateResult.Value.OwnerId,
            updateResult.Value.Name,
            updateResult.Value.Description,
            updateResult.Value.StatusName,
            updateResult.Value.Stock,
            updateResult.Value.Price,
            discount.Amount,
            await updateResult.Value.FinalPrice(),
            updateResult.Value.CreatedAtUtc));
    }
}