// <copyright file="ProductsController.cs" company="Tek">
// Copyright (c) TekChallenge.Services.ProductService. All rights reserved.
// </copyright>

using FluentResults;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using TekChallenge.Services.ProductService.Application.Products.Commands.AddProduct;
using TekChallenge.Services.ProductService.Application.Products.Commands.DeleteProduct;
using TekChallenge.Services.ProductService.Application.Products.Commands.UpdateProduct;
using TekChallenge.Services.ProductService.Application.Products.Queries.GetProductById;
using TekChallenge.Services.ProductService.Application.Products.Queries.GetProductsList;
using TekChallenge.Services.ProductService.Application.Products.Dtos;
using TekChallenge.Services.ProductService.Contracts.Product.Requests;
using TekChallenge.Services.ProductService.Contracts.Product.Responses;
using TekChallenge.SharedDefinitions.Presentation.Controllers;
using TekChallenge.SharedDefinitions.Application.Abstractions.Services;
using TekChallenge.SharedDefinitions.Presentation.Attributes;
using TekChallenge.Services.AuthService.Contracts.Enums;

namespace TekChallenge.Services.ProductService.API.Controllers;

/// <summary>
/// Products Controller.
/// </summary>
[Route("[controller]")]
public class ProductsController : ResultControllerBase<ProductsController>
{
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;

    /// <summary>
    /// Initializes a new instance of the <see cref="ProductsController"/> class.
    /// </summary>
    /// <param name="mediator">Injected _mediator.</param>
    /// <param name="mapper">Injected _mapper.</param>
    /// <param name="logger">Injected _logger.</param>
    /// <param name="exceptionHandlingService">Injected _exceptionHandlingService.</param>
    public ProductsController(IMediator mediator, IMapper mapper, ILogger<ProductsController> logger, IExceptionHandlingService exceptionHandlingService)
        : base(mediator, mapper, logger, exceptionHandlingService)
    {
        _mediator = mediator;
        _mapper = mapper;
    }

    /// <summary>
    /// Gets a list of Products.
    /// </summary>
    /// <returns>The list of Products.</returns>
    [HttpGet]
    [RoleAuthorize]
    public async Task<IActionResult> GetProducts() =>
        await HandleRequestAsync<GetProductsListQuery, List<ProductDto>, List<ProductResponse>>();

    /// <summary>
    /// Gets a Product by its Id.
    /// </summary>
    /// <param name="id">Product Id.</param>
    /// <returns>The Product Aggregate.</returns>
    [HttpGet("{id:long}")]
    [RoleAuthorize]
    public async Task<IActionResult> GetProductById(long id) =>
        await HandleRequestAsync<GetProductByIdQuery, ProductDto, ProductResponse>(id);

    /// <summary>
    /// Add a Product to the Product Repository.
    /// </summary>
    /// <param name="request">Product data.</param>
    /// <returns>The Product instance created with Id.</returns>
    [HttpPost]
    [RoleAuthorize(Roles.Admin)]
    public async Task<IActionResult> AddProduct(AddProductRequest request) =>
        await HandleRequestAsync<AddProductCommand, ProductDto, ProductResponse>(request);

    /// <summary>
    /// Updates a Product in the Product Repository.
    /// </summary>
    /// <param name="request">Product data.</param>
    /// <returns>The Product instance created with Id.</returns>
    [HttpPut]
    [RoleAuthorize(Roles.Admin)]
    public async Task<IActionResult> UpdateProduct(UpdateProductRequest request) =>
        await HandleRequestAsync<UpdateProductCommand, ProductDto, ProductResponse>(request);

    /// <summary>
    /// Deletes a Product from the Product Repository.
    /// </summary>
    /// <param name="id">Product Id.</param>
    /// <returns>The Action Result of the delete operation.</returns>
    [HttpDelete("{id:long}")]
    [RoleAuthorize(Roles.Admin)]
    public async Task<IActionResult> DeleteProduct(long id) =>
        await HandleRequestAsync<DeleteProductCommand, Result, object>(id);
}
