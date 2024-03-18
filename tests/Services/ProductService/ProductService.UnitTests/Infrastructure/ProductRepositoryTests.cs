using Microsoft.Extensions.Logging;
using System.Data;
using TekChallenge.SharedDefinitions.Infrastructure.Abstractions;
using System.Data.Common;
using TekChallenge.Services.ProductService.Application.Abstractions.Repositories;
using TekChallenge.Services.ProductService.Infrastructure.Repositories;
using Dapr.Client;
using TekChallenge.SharedDefinitions.Application.Abstractions.Services;
using NSubstitute;
using TekChallenge.Services.ProductService.Domain.Products.ValueObjects;
using TekChallenge.Services.ProductService.Domain.Products;
using TekChallenge.Services.ProductService.Infrastructure.DataTransferObjects;

namespace TekChallenge.Tests.Services.ProductService.Infrastructure;

public class ProductRepositoryTests
{
    private readonly IProductRepository _userRepository;
    private readonly IDapperUtility _dapper;

    public ProductRepositoryTests()
    {
        _dapper = Substitute.For<IDapperUtility>();
        var connectionFactory = Substitute.For<ISqlConnectionFactory<DbConnection>>();
        var logger = Substitute.For<ILogger<ProductRepository>>();
        var daprClient = Substitute.For<DaprClient>();
        var cacheService = Substitute.For<ICacheService>();
        _userRepository = new ProductRepository(_dapper, connectionFactory, logger, daprClient, cacheService);
    }

    [Fact]
    public async Task GetByIdAsync_ExistingProduct_ReturnsProduct()
    {
        // Arrange
        var productId = new ProductId(1);
        var dbProduct = new ProductDb(1, 1, "Test Product", "Test Description", 0, 0, 0, DateTime.UtcNow);
        _dapper.QueryFirstOrDefaultAsync<ProductDb>(Arg.Any<string>(), Arg.Any<object>(), Arg.Any<CommandType>(), Arg.Any<DbTransaction?>()).Returns(Task.FromResult<ProductDb?>(dbProduct));

        // Act
        var result = await _userRepository.GetByIdAsync(productId);

        // Assert
        result.IsSuccess.Should().BeTrue();
        result.Value.Id.Should().Be(productId);
        result.Value.Name.Should().Be("Test Product");
        result.Value.Description.Should().Be("Test Description");
    }

    [Fact]
    public async Task GetAllAsync_ReturnsAllProducts()
    {
        // Arrange
        var products = new List<ProductDb>
        {
            new ProductDb(1, 1, "Test Product 1", "Test Description", 0, 0, 0, DateTime.UtcNow),
            new ProductDb(2, 1, "Test Product 2", "Test Description", 0, 0, 0, DateTime.UtcNow),
        };
        _dapper.QueryAsync<ProductDb>(Arg.Any<string>()).Returns(Task.FromResult<IEnumerable<ProductDb>>(products));

        // Act
        var result = await _userRepository.GetAllAsync();

        // Assert
        result.IsSuccess.Should().BeTrue();
        result.Value.Should().HaveCount(2);
    }

    [Fact]
    public async Task AddAsync_ValidProduct_ReturnsAddedProduct()
    {
        // Arrange
        var newProduct = Product.Create(null, "New Product");
        _dapper
            .ExecuteScalarAsync(Arg.Any<string>(), Arg.Any<object>(), Arg.Any<CommandType>(), Arg.Any<DbTransaction?>())
            .Returns(Task.FromResult<long>(1));
        _dapper
            .QueryFirstOrDefaultAsync<ProductDb>(Arg.Any<string>(), Arg.Any<object>(), Arg.Any<CommandType>(), Arg.Any<DbTransaction?>())
            .Returns(Task.FromResult<ProductDb?>(new ProductDb(1, 1, "New Product", "", 0, 0, 0, DateTime.UtcNow)));

        // Act
        var result = await _userRepository.AddAsync(newProduct.Value);

        // Assert
        result.IsSuccess.Should().BeTrue();
        result.Value.Id.Value.Should().Be(1);
        result.Value.Name.Should().Be("New Product");
    }

    [Fact]
    public async Task UpdateAsync_ExistingProduct_ReturnsUpdatedProduct()
    {
        // Arrange
        var existingProduct = Product.Create(new ProductId(1), "Existing Product");
        var dbProduct = new ProductDb(1, 1, "Existing Product", "", 0, 0, 0, DateTime.UtcNow);
        _dapper
            .BeginTransaction()
            .Returns(Substitute.For<DbTransaction>());
        _dapper
            .QueryFirstOrDefaultAsync<ProductDb>(Arg.Any<string>(), Arg.Any<object>(), Arg.Any<CommandType>(), Arg.Any<DbTransaction?>())
            .Returns(Task.FromResult<ProductDb?>(dbProduct));
         _dapper
            .ExecuteAsync(Arg.Any<string>(), Arg.Any<object>(), Arg.Any<CommandType>(), Arg.Any<DbTransaction?>())
            .Returns(Task.FromResult<long>(1));

        // Act
        var result = await _userRepository.UpdateAsync(existingProduct.Value);

        // Assert
        result.IsSuccess.Should().BeTrue();
        result.Value.Name.Should().Be("Existing Product");
    }

    [Fact]
    public async Task RemoveAsync_ExistingProductId_RemovesProduct()
    {
        // Arrange
        var productId = new ProductId(1);
        _dapper
             .BeginTransaction()
             .Returns(Substitute.For<DbTransaction>());
        _dapper
             .ExecuteAsync(Arg.Any<string>(), Arg.Any<object>(), Arg.Any<CommandType>(), Arg.Any<DbTransaction?>())
             .Returns(Task.FromResult<long>(1));

        // Act
        var result = await _userRepository.RemoveAsync(productId);

        // Assert
        result.IsSuccess.Should().BeTrue();
    }
}