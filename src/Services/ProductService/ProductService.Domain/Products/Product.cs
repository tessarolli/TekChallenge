// <copyright file="Product.cs" company="Tek">
// Copyright (c) TekChallenge.Services.ProductService. All rights reserved.
// </copyright>

using FluentResults;
using SharedDefinitions.Domain.Common.Exceptions;
using TekChallenge.Services.ProductService.Domain.Dtos;
using TekChallenge.Services.ProductService.Domain.Enums;
using TekChallenge.Services.ProductService.Domain.Products.Dtos;
using TekChallenge.Services.ProductService.Domain.Products.Validators;
using TekChallenge.Services.ProductService.Domain.Products.ValueObjects;
using TekChallenge.SharedDefinitions.Domain.Common.DDD;

namespace TekChallenge.Services.ProductService.Domain.Products;

/// <summary>
/// Product Entity.
/// </summary>
public sealed class Product : Entity<ProductId>
{
    private Product(ProductId id)
        : base(id)
    {
        Id = id;
        StatusName = StatusName.Inactive;
    }

    /// <summary>
    /// Gets the Product's Name.
    /// </summary>
    public string Name { get; init; } = null!;

    /// <summary>
    /// Gets the Product's Owner Id.
    /// </summary>
    public long OwnerId { get; init; }

    /// <summary>
    /// Gets Product's Creation Date on UTC.
    /// </summary>
    public DateTime CreatedAtUtc { get; init; }

    /// <summary>
    /// Gets the Product's Status.
    /// </summary>
    public StatusName StatusName { get; init; }

    /// <summary>
    /// Gets the Product's Quantity in Stock.
    /// </summary>
    public int Stock { get; init; }

    /// <summary>
    /// Gets the Product's Description.
    /// </summary>
    public string Description { get; init; } = null!;

    /// <summary>
    /// Gets the Product's Price.
    /// </summary>
    public decimal Price { get; init; }

    /// <summary>
    /// Gets the Product's Discount Percentage.
    /// </summary>
    public Lazy<Task<DiscountDto?>>? Discount { get; init; }

    /// <summary>
    /// Gets the Product's Owner <see cref="UserDto"/>.
    /// </summary>
    public Lazy<Task<UserDto?>>? Owner { get; init; } = null!;

    /// <summary>
    /// Calculates and gets the Product's Final Price.
    /// </summary>
    public async Task<decimal> FinalPrice()
    {
        if (Discount is null)
        {
            throw new NullReferenceException(nameof(Discount));
        }

        var discount = await Discount.Value;
        if (discount is null)
        {
            throw new UnreachableExternalServiceException("Discount Service");
        }

        return Price * (100 - discount.Amount) / 100;
    }

    /// <summary>
    /// Product Creation Factory.
    /// </summary>
    /// <param name="id">The Product's Id.</param>
    /// <param name="name">The Product's Name.</param>
    /// <param name="description">The Product's Description.</param>
    /// <param name="stock">The Product's Quantity in Stock.</param>
    /// <param name="price">The Product's Base Price.</param>
    /// <param name="statusName">The Product's Status.</param>
    /// <param name="ownerId">The Product's Owner User Id.</param>
    /// <param name="createdAtUtc">The Product's Creation Date.</param>
    /// <param name="ownerLazyLoader">The Lazy Load Implementation for the fetching the Product's Owner <see cref="UserDto"/>.</param>
    /// <param name="discountLazyLoader">The Lazy Load Implementation for the fetching the Product's Discount <see cref="DiscountDto"/>.</param>
    /// <returns>Product Domain Instance.</returns>
    public static Result<Product> Create(
        ProductId? id,
        string name,
        string description = "",
        int stock = 0,
        decimal price = 0,
        StatusName statusName = StatusName.Inactive,
        long? ownerId = null,
        DateTime? createdAtUtc = null,
        Lazy<Task<UserDto?>>? ownerLazyLoader = null,
        Lazy<Task<DiscountDto?>>? discountLazyLoader = null)
    {
        var Product = new Product(id ?? new ProductId())
        {
            Name = name,
            Description = description,
            Stock = stock,
            Price = price,
            StatusName = statusName,
            OwnerId = ownerId ?? -1,
            CreatedAtUtc = createdAtUtc ?? DateTime.UtcNow.AddYears(-100),
            Owner = ownerLazyLoader,
            Discount = discountLazyLoader,
        };

        var validationResult = Product.Validate();
        if (!validationResult.IsSuccess)
        {
            return Result.Fail(validationResult.Errors);
        }

        return Result.Ok(Product);
    }

    /// <inheritdoc/>
    protected override object GetValidator() => new ProductValidator();
}
