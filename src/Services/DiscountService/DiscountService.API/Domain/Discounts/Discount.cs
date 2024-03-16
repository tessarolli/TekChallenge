// <copyright file="ProductId.cs" company="Tek">
// Copyright (c) TekChallenge.Services.ProductService. All rights reserved.
// </copyright>

using TekChallenge.SharedDefinitions.Domain.Common.DDD;

namespace TekChallenge.Services.ProductService.Domain.Products.ValueObjects;

/// <summary>
/// Product Id Value Object.
/// </summary>
public sealed class Discount : ValueObject
{
    /// <summary>
    /// Initializes a new instance of the <see cref="ProductId"/> class.
    /// </summary>
    /// <param name="id">Id value if exists.</param>
    /// <param name="amount">Id value if exists.</param>
    public Discount(long? productId = null, int amount = 0)
    {
        ProductId = productId ?? 0;
        Amount = amount;
    }

    /// <summary>
    /// Gets the Product ID.
    /// </summary>
    public long ProductId { get; }

    /// <summary>
    /// Gets the Discount Amount.
    /// </summary>
    public int Amount { get; }

    /// <summary>
    /// Method required for comparing value objects.
    /// </summary>
    /// <returns>An ienumerable with all the properties of the value object.</returns>
    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return ProductId;
        yield return Amount;
    }
}
