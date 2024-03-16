// <copyright file="ProductId.cs" company="Tek">
// Copyright (c) TekChallenge.Services.ProductService. All rights reserved.
// </copyright>

using TekChallenge.SharedDefinitions.Domain.Common.DDD;

namespace TekChallenge.Services.ProductService.Domain.Products.ValueObjects;

/// <summary>
/// Product Id Value Object.
/// </summary>
public sealed class ProductId : ValueObject
{
    /// <summary>
    /// Initializes a new instance of the <see cref="ProductId"/> class.
    /// </summary>
    /// <param name="id">Id value if exists.</param>
    public ProductId(long? id = null)
    {
        Value = id ?? 0;
    }

    /// <summary>
    /// Gets the Product ID.
    /// </summary>
    public long Value { get; }

    /// <summary>
    /// Method required for comparing value objects.
    /// </summary>
    /// <returns>An ienumerable with all the properties of the value object.</returns>
    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}
