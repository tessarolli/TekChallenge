// <copyright file="ProductRepository.cs" company="Tek">
// Copyright (c) TekChallenge.Services.ProductService. All rights reserved.
// </copyright>

using System.Data;
using System.Data.Common;
using Dapr.Client;
using FluentResults;
using Microsoft.Extensions.Logging;
using TekChallenge.Services.ProductService.Application.Abstractions.Repositories;
using TekChallenge.Services.ProductService.Domain.Products;
using TekChallenge.Services.ProductService.Domain.Products.ValueObjects;
using TekChallenge.SharedDefinitions.Infrastructure.Utilities;
using TekChallenge.SharedDefinitions.Infrastructure.Abstractions;
using TekChallenge.Services.ProductService.Infrastructure.DataTransferObjects;
using TekChallenge.Services.ProductService.Domain.Products.Dtos;
using TekChallenge.Services.ProductService.Domain.Dtos;
using TekChallenge.Services.ProductService.Domain.Enums;
using TekChallenge.SharedDefinitions.Application.Common.Errors;
using TekChallenge.SharedDefinitions.Application.Abstractions.Services;

namespace TekChallenge.Services.ProductService.Infrastructure.Repositories;

/// <summary>
/// The Product Repository Implementation.
/// </summary>
public class ProductRepository : IProductRepository
{
    private readonly ICacheService _cache;
    private readonly IDapperUtility _db;
    private readonly DaprClient _dapr;
    private readonly ILogger _logger;

    /// <summary>
    /// Initializes a new instance of the <see cref="ProductRepository"/> class.
    /// </summary>
    /// <param name="dapperUtility">IDapperUtility to inject.</param>
    /// <param name="connectionFactory">ISqlConnectionFactory to inject.</param>
    /// <param name="logger">ILogger to inject.</param>
    /// <param name="dapr">DaprClient to inject.</param>
    /// <param name="cache">ICacheService to inject.</param>
    public ProductRepository(IDapperUtility dapperUtility, ISqlConnectionFactory<DbConnection> connectionFactory, ILogger<ProductRepository> logger, DaprClient dapr, ICacheService cache)
    {
        _logger = logger;
        _db = dapperUtility;
        _dapr = dapr;
        _cache = cache;
    }

    /// <inheritdoc/>
    public async Task<Result<Product>> GetByIdAsync(ProductId id, DbTransaction? transaction = null)
    {
        _logger.LogInformation("ProductRepository.GetByIdAsync({id})", id.Value);

        const string ProductSql = @"
            SELECT 
                *
            FROM 
                Products
            WHERE 
                id = @Id;";

        var Product = await _db.QueryFirstOrDefaultAsync<ProductDb>(ProductSql, new { Id = id.Value }, transaction: transaction);

        if (Product == null)
        {
            return new NotFoundError($"Product with id {id.Value} not found.");
        }

        var mapped = await CreateProductResultFromProductDtoAsync(Product);
        if (!mapped.IsSuccess)
        {
            return Result.Fail(mapped.Errors);
        }

        return mapped.Value;
    }

    /// <inheritdoc/>
    public async Task<Result<List<Product>>> GetAllAsync()
    {
        _logger.LogInformation("ProductRepository.GetAllAsync");

        const string ProductSql = @"
            SELECT 
                *
            FROM 
                Products";

        var productDbs = await _db.QueryAsync<ProductDb>(ProductSql);

        var results = await Task.WhenAll(productDbs.Select(async productDb => await CreateProductResultFromProductDtoAsync(productDb)));

        var validResults = results.Where(mapped => mapped.IsSuccess).Select(mapped => mapped.Value).ToList();

        return Result.Ok(validResults);
    }

    /// <inheritdoc/>
    public async Task<Result<Product>> AddAsync(Product Product)
    {
        _logger.LogInformation("ProductRepository.AddAsync({Product})", Product.Name);

        using var trans = _db.BeginTransaction();

        try
        {
            var insertSql = @"
            INSERT INTO 
                Products (name, owner_id, description, status_name, stock, price, created_at_utc)
            VALUES 
                (@Name, @OwnerId, @Description, @StatusName, @Stock, @Price, @CreatedAtUtc)
            RETURNING
                id";

            var parameters = new
            {
                Product.Name,
                Product.OwnerId,
                Product.Description,
                Product.StatusName,
                Product.Stock,
                Product.Price,
                Product.CreatedAtUtc
            };

            var newProductId = await _db.ExecuteScalarAsync(insertSql, parameters, transaction: trans);

            _db.CloseTransaction(trans, true);

            return await GetByIdAsync(new ProductId(newProductId));
        }
        catch (Exception e)
        {
            _logger.LogError(e, null);

            trans.Rollback();

            _db.CloseTransaction(trans);

            throw;
        }
    }

    /// <inheritdoc/>
    public async Task<Result<Product>> UpdateAsync(Product Product)
    {
        _logger.LogInformation("ProductRepository.UpdateAsync({Product})", Product.Name);

        using var trans = _db.BeginTransaction();

        try
        {
            var updateSql = @"
                UPDATE 
                    Products
                SET 
                    name = @Name, 
                    owner_id = @OwnerId,
                    description = @Description,
                    status_name = @StatusName,
                    stock = @Stock,
                    price = @Price,
                    created_at_utc = @CreatedAtUtc
                WHERE 
                    id = @Id";

            var parameters = new
            {
                Id = Product.Id.Value,
                Product.Name,
                Product.OwnerId,
                Product.Description,
                Product.StatusName,
                Product.Stock,
                Product.Price,
                Product.CreatedAtUtc
            };

            await _db.ExecuteAsync(updateSql, parameters, transaction: trans);

            trans.Commit();
        }
        catch (Exception e)
        {
            _logger.LogError(e, null);

            trans.Rollback();

            throw;
        }

        _db.CloseTransaction(trans);

        return await GetByIdAsync(Product.Id);
    }

    /// <inheritdoc/>
    public async Task<Result> RemoveAsync(ProductId productId)
    {
        _logger.LogInformation("ProductRepository.RemoveAsync({Product})", productId);

        using var trans = _db.BeginTransaction();

        try
        {
            var deleteDocumentAccessSql = @"
                DELETE FROM 
                    Products
                WHERE 
                    id = @productId";

            var parameter = new { ProductId = productId.Value };

            await _db.ExecuteAsync(deleteDocumentAccessSql, parameter, transaction: trans);

            trans.Commit();
        }
        catch (Exception e)
        {
            _logger.LogError(e, null);

            trans.Rollback();

            throw;
        }

        _db.CloseTransaction(trans);

        return Result.Ok();
    }

    /// <summary>
    /// Map the Product Data Transfer Object to a Result of Product Domain Model.
    /// </summary>
    /// <param name="product">Product Db Dto.</param>
    /// <returns>The Result of Product Domain Model.</returns>
    private async Task<Result<Product>> CreateProductResultFromProductDtoAsync(ProductDb product)
    {
        var productDomainModel = Product.Create(
                new ProductId(product.id),
                product.name,
                product.description,
                product.stock,
                product.price,
                await GetProductStatusName(product),
                product.owner_id,
                product.created_at_utc,
                GetOwnerLazyLoader(product),
                GetDiscountLazyLoader(product));

        if (!productDomainModel.IsSuccess)
        {
            return Result.Fail(productDomainModel.Errors);
        }

        return productDomainModel;
    }

    private async Task<StatusName> GetProductStatusName(ProductDb product)
    {
        int status;

        var statusFromCache = await _cache.GetAsync<int?>(product.id.ToString());
        if (statusFromCache is null)
        {
            status = new Random().Next(0,2);
            
            await _cache.SetAsync<int?>(product.id.ToString(), status);
        }
        else
        {
            status = statusFromCache.Value;
        }

        return (StatusName)status;
    }

    private Lazy<Task<UserDto?>> GetOwnerLazyLoader(ProductDb product)
    {
        return new Lazy<Task<UserDto?>>(async () =>
        {
            return await _dapr.InvokeMethodAsync<UserDto>("auth-service-api", $"/authentication/getuserbyid/{product.owner_id}");
        });
    }

    private Lazy<Task<DiscountDto?>> GetDiscountLazyLoader(ProductDb product)
    {
        return new Lazy<Task<DiscountDto?>>(async () =>
        {
            try
            {
                return await _dapr.InvokeMethodAsync<DiscountDto>(HttpMethod.Get, "discount-service-api", $"discount/{product.id}/");
            }
            catch (Exception)
            {
                return null;
            }
        });
    }
}
