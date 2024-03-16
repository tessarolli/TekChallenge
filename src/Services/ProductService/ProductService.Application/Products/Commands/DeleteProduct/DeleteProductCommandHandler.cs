// <copyright file="DeleteProductCommandHandler.cs" company="Tek">
// Copyright (c) TekChallenge.Services.ProductService. All rights reserved.
// </copyright>

using FluentResults;
using TekChallenge.Services.ProductService.Application.Abstractions.Repositories;
using TekChallenge.Services.ProductService.Domain.Products.ValueObjects;
using TekChallenge.SharedDefinitions.Application.Abstractions.Messaging;

namespace TekChallenge.Services.ProductService.Application.Products.Commands.DeleteProduct;

/// <summary>
/// Mediator Handler for the <see cref="DeleteProductCommand"/>.
/// </summary>
public class DeleteProductCommandHandler : ICommandHandler<DeleteProductCommand>
{
    private readonly IProductRepository _productRepository;

    /// <summary>
    /// Initializes a new instance of the <see cref="DeleteProductCommandHandler"/> class.
    /// </summary>
    /// <param name="ProductRepository">Injected UserRepository.</param>
    public DeleteProductCommandHandler(IProductRepository ProductRepository)
    {
        _productRepository = ProductRepository;
    }

    /// <inheritdoc/>
    public async Task<Result> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
    {
        return await _productRepository.RemoveAsync(new ProductId(request.Id));
    }
}
