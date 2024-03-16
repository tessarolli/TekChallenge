// <copyright file="GetProductByIdQueryHandler.cs" company="Tek">
// Copyright (c) TekChallenge.Services.ProductService. All rights reserved.
// </copyright>

using FluentResults;
using TekChallenge.Services.ProductService.Application.Abstractions.Repositories;
using TekChallenge.Services.ProductService.Application.Products.Dtos;
using TekChallenge.Services.ProductService.Domain.Products.ValueObjects;
using TekChallenge.SharedDefinitions.Application.Abstractions.Messaging;
using TekChallenge.SharedDefinitions.Application.Common.Errors;

namespace TekChallenge.Services.ProductService.Application.Products.Queries.GetProductById;

/// <summary>
/// Mediator Handler for the <see cref="GetProductByIdQuery"/>.
/// </summary>
public class GetProductByIdQueryHandler : IQueryHandler<GetProductByIdQuery, ProductDto>
{
    private readonly IProductRepository _productRepository;

    /// <summary>
    /// Initializes a new instance of the <see cref="GetProductByIdQueryHandler"/> class.
    /// </summary>
    /// <param name="ProductRepository">Injected UserRepository.</param>
    public GetProductByIdQueryHandler(IProductRepository ProductRepository)
    {
        _productRepository = ProductRepository;
    }

    /// <inheritdoc/>
    public async Task<Result<ProductDto>> Handle(GetProductByIdQuery query, CancellationToken cancellationToken)
    {
        var productDomainModel = await _productRepository.GetByIdAsync(new ProductId(query.Id));
        if (!productDomainModel.IsSuccess)
        {
            return Result.Fail(productDomainModel.Errors);
        }

        var discountTask = productDomainModel.Value.Discount?.Value;
        if (discountTask is null)
        {
            return Result.Fail(new UnreachableExternalServiceError("Discount Service"));
        }

        return Result.Ok(new ProductDto(
                productDomainModel.Value.Id.Value,
                productDomainModel.Value.OwnerId,
                productDomainModel.Value.Name,
                productDomainModel.Value.Description,
                productDomainModel.Value.StatusName,
                productDomainModel.Value.Stock,
                productDomainModel.Value.Price,
                (await discountTask)?.Amount ?? 0,
                await productDomainModel.Value.FinalPrice(),
                productDomainModel.Value.CreatedAtUtc));
    }
}