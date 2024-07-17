using TekChallenge.Services.ProductService.Application.Abstractions.Repositories;
using TekChallenge.Services.ProductService.Application.Products.Commands.AddProduct;
using TekChallenge.Services.ProductService.Application.Products.Commands.DeleteProduct;
using TekChallenge.Services.ProductService.Application.Products.Commands.UpdateProduct;
using TekChallenge.Services.ProductService.Application.Products.Dtos;
using TekChallenge.Services.ProductService.Application.Products.Queries.GetProductById;
using TekChallenge.Services.ProductService.Application.Products.Queries.GetProductsList;
using TekChallenge.Services.ProductService.Domain.Dtos;
using TekChallenge.Services.ProductService.Domain.Products;
using TekChallenge.Services.ProductService.Domain.Products.ValueObjects;

namespace TekChallenge.Tests.Services.ProductService;

public class ProductsCommandAndQueriesTests
{
    private readonly IProductRepository _productRepository;

    public ProductsCommandAndQueriesTests()
    {
        _productRepository = Substitute.For<IProductRepository>();
    }

    [Fact]
    public async Task Handle_ValidRequest_ReturnProductDto()
    {
        // Arrange
        var utcNow = DateTime.UtcNow;
        var productDto = new ProductDto(1, 1, "Product", "Description", 0, 0, 0, 0, 0, utcNow);
        var addProductCommand = new AddProductCommand(1, "Product", "Description", 0);
        var lazyLoader = new Lazy<Task<DiscountDto>>(() => Task.FromResult(new DiscountDto(0, 0)));
        var productDomainModel = Product.Create(new ProductId(1), "Product", "Description", 0, 0, ownerId: 1, discountLazyLoader: lazyLoader!, createdAtUtc: utcNow);
        _productRepository.AddAsync(Arg.Any<Product>()).Returns(Result.Ok(productDomainModel.Value));

        var handler = new AddProductCommandHandler(_productRepository);

        // Act
        var result = await handler.Handle(addProductCommand, CancellationToken.None);

        // Assert
        result.IsSuccess.Should().BeTrue();
        result.Value.Should().BeEquivalentTo(productDto);
    }

    [Fact]
    public async Task Handle_InvalidRequest_ReturnError()
    {
        // Arrange
        var addProductCommand = new AddProductCommand(1, "Product", "Description", 0);
        var error = new Error("Invalid Product Data");
        _ = Product.Create(null, "Product", "Description", 0, 0);
        _productRepository.AddAsync(Arg.Any<Product>()).Returns(Result.Fail(error));
        var handler = new AddProductCommandHandler(_productRepository);

        // Act
        var result = await handler.Handle(addProductCommand, CancellationToken.None);

        // Assert
        result.IsFailed.Should().BeTrue();
        result.Errors.Should().ContainEquivalentOf(error);
    }

    [Fact]
    public async Task Handle_ValidId_ReturnsSuccessResult()
    {
        // Arrange
        long productId = 1;
        var deleteCommand = new DeleteProductCommand(productId);
        var handler = new DeleteProductCommandHandler(_productRepository);
        _productRepository.RemoveAsync(Arg.Any<ProductId>()).Returns(Result.Ok());

        // Act
        var result = await handler.Handle(deleteCommand, CancellationToken.None);

        // Assert
        result.IsSuccess.Should().BeTrue();
    }

    [Fact]
    public async Task Handle_InvalidId_ReturnsFailureResult()
    {
        // Arrange
        long productId = 0;
        var deleteCommand = new DeleteProductCommand(productId);
        var handler = new DeleteProductCommandHandler(_productRepository);
        var error = new Error("Product not found");
        _productRepository.RemoveAsync(Arg.Any<ProductId>()).Returns(Result.Fail("Product not found"));

        // Act
        var result = await handler.Handle(deleteCommand, CancellationToken.None);

        // Assert
        result.IsSuccess.Should().BeFalse();
        result.Errors.Should().ContainEquivalentOf(error);
    }

    [Fact]
    public async Task Handle_RepositoryException_ReturnsFailureResult()
    {
        // Arrange
        long productId = 2;
        var deleteCommand = new DeleteProductCommand(productId);
        var handler = new DeleteProductCommandHandler(_productRepository);
        var error = new Error("Repository exception");
        _productRepository.RemoveAsync(Arg.Any<ProductId>()).Returns(Result.Fail("Repository exception"));

        // Act
        var result = await handler.Handle(deleteCommand, CancellationToken.None);

        // Assert
        result.IsSuccess.Should().BeFalse();
        result.Errors.Should().ContainEquivalentOf(error);
    }

    [Fact]
    public async Task Handle_ValidRequest_ReturnsProductDto()
    {
        // Arrange
        var handler = new UpdateProductCommandHandler(_productRepository);
        var request = new UpdateProductCommand(1, 1, "Product", "Description", 100, 5);
        var lazyLoader = new Lazy<Task<DiscountDto>>(() => Task.FromResult(new DiscountDto(0,0)));
        var productDomainModel = Product.Create(new ProductId(1), "Product", "Description", 5, 100, ownerId: 1, discountLazyLoader: lazyLoader!);

        _productRepository.UpdateAsync(Arg.Any<Product>()).Returns(productDomainModel);

        // Act
        var result = await handler.Handle(request, CancellationToken.None);

        // Assert
        result.Should().NotBeNull();
        result.IsSuccess.Should().BeTrue();
        result.Value.Should().NotBeNull();
        result.Value.Id.Should().Be(1);
        result.Value.OwnerId.Should().Be(1);
        result.Value.Name.Should().Be("Product");
        result.Value.Description.Should().Be("Description");
        result.Value.Stock.Should().Be(5);
        result.Value.Price.Should().Be(100);
    }

    [Fact]
    public async Task Handle_InvalidRequest_ReturnsFailureResult()
    {
        // Arrange
        var handler = new UpdateProductCommandHandler(_productRepository);
        var request = new UpdateProductCommand(1, 1, "Product", "Description", 100, 5);
        var productDomainModel = Result.Fail<Product>("Invalid data.");

        _productRepository.UpdateAsync(Arg.Any<Product>()).Returns(productDomainModel);

        // Act
        var result = await handler.Handle(request, CancellationToken.None);

        // Assert
        result.Should().NotBeNull();
        result.IsSuccess.Should().BeFalse();
        result.Errors.Should().NotBeEmpty();
    }

    [Fact]
    public async Task Handle_ValidId_ReturnsProductDto()
    {
        // Arrange
        var productId = 1;
        var utcNow = DateTime.UtcNow;
        var getProductByIdQuery = new GetProductByIdQuery(productId);
        var lazyLoader = new Lazy<Task<DiscountDto>>(() => Task.FromResult(new DiscountDto(0,0)));
        var productDto = new ProductDto(productId, productId, "Product", "Description", 0, 0, 0, 0, 0, utcNow);
        _productRepository.GetByIdAsync(new ProductId(productId)).Returns(Result.Ok(Product.Create(new ProductId(productId), "Product", "Description", 0, 0, createdAtUtc: utcNow, ownerId: 1, discountLazyLoader: lazyLoader!).Value));
        var handler = new GetProductByIdQueryHandler(_productRepository);

        // Act
        var result = await handler.Handle(getProductByIdQuery, CancellationToken.None);

        // Assert
        result.Should().NotBeNull();
        result.IsSuccess.Should().BeTrue();
        result.Value.Should().BeEquivalentTo(productDto);
    }

    [Fact]
    public async Task Handle_InvalidId_ReturnsErrorResult()
    {
        // Arrange
        var productId = 0;
        var getProductByIdQuery = new GetProductByIdQuery(productId);
        _productRepository.GetByIdAsync(new ProductId(productId)).Returns(Result.Fail(new Error("Product not found")));
        var handler = new GetProductByIdQueryHandler(_productRepository);

        // Act
        var result = await handler.Handle(getProductByIdQuery, CancellationToken.None);

        // Assert
        result.Should().NotBeNull();
        result.IsFailed.Should().BeTrue();
        result.Errors.Should().ContainSingle().Which.Message.Should().Be("Product not found");
    }

    [Fact]
    public void Validate_ValidId_NoValidationErrors()
    {
        // Arrange
        var getProductByIdQueryValidator = new GetProductByIdQueryValidator();
        var getProductByIdQuery = new GetProductByIdQuery(1);

        // Act
        var result = getProductByIdQueryValidator.Validate(getProductByIdQuery);

        // Assert
        result.IsValid.Should().BeTrue();
    }

    [Fact]
    public async Task Handle_WithValidData_ShouldReturnListOfProductDtos()
    {
        // Arrange
        var userRepository = Substitute.For<IProductRepository>();
        var productsListQuery = new GetProductsListQuery();
        var productsListQueryHandler = new GetProductsListQueryHandler(userRepository);
        var lazyLoader = new Lazy<Task<DiscountDto>>(() => Task.FromResult(new DiscountDto(0, 0)));

        var mockedProducts = new List<Product>
        {
            Product.Create(new ProductId(1), "Product", "Description", 0, 100, ownerId: 1, discountLazyLoader : lazyLoader !).Value,
        };

        userRepository.GetAllAsync().Returns(Result.Ok(mockedProducts));

        // Act
        var result = await productsListQueryHandler.Handle(productsListQuery, CancellationToken.None);

        // Assert
        result.IsSuccess.Should().BeTrue();
        result.Value.Should().NotBeNull();
        result.Value.Should().HaveCount(1);
        result.Value.First().Name.Should().Be("Product");
        result.Value.First().Description.Should().Be("Description");
        result.Value.First().OwnerId.Should().Be(1);
        result.Value.First().Price.Should().Be(100);
    }

    [Fact]
    public async Task Handle_WithNoProducts_ShouldReturnEmptyList()
    {
        // Arrange
        var userRepository = Substitute.For<IProductRepository>();
        var productsListQuery = new GetProductsListQuery();
        var productsListQueryHandler = new GetProductsListQueryHandler(userRepository);

        userRepository.GetAllAsync().Returns(Result.Ok(new List<Product>()));

        // Act
        var result = await productsListQueryHandler.Handle(productsListQuery, CancellationToken.None);

        // Assert
        result.IsSuccess.Should().BeTrue();
        result.Value.Should().NotBeNull();
        result.Value.Should().BeEmpty();
    }
}