// <copyright file="DeleteProductCommand.cs" company="Tek">
// Copyright (c) TekChallenge.Services.ProductService. All rights reserved.
// </copyright>


using TekChallenge.SharedDefinitions.Application.Abstractions.Messaging;

namespace TekChallenge.Services.ProductService.Application.Products.Commands.DeleteProduct;

/// <summary>
/// Command to remove a product from the catalog.
/// </summary>
/// <param name="Id">The Id of the product being deleted.</param>
public record DeleteProductCommand(long Id) : ICommand;
