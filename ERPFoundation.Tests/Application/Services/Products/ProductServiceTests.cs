using ERPFoundation.Domain.Exceptions;
using ERPFoundation.Domain.Models;
using ERPFoundation.Tests.Builders;
using Moq;

namespace ERPFoundation.Tests.Application.Services.Products;

public class ProductServiceTests : ProductServiceTestsBase
{
    [Fact]
    public async Task CreateProductAsync_WhenProductIsValid_ShouldCreateSuccessfully()
    {
        var product = new ProductBuilder()
            .WithValidData()
            .Build();

        ProductRepositoryMock
            .Setup(r => r.ExistsSkuAsync(product.Sku))
            .ReturnsAsync(false);

        ProductRepositoryMock
            .Setup(r => r.ExistsSupplierAsync(product.SupplierId))
            .ReturnsAsync(true);

        ProductRepositoryMock
            .Setup(r => r.AddProductsAsync(product))
            .Returns(Task.CompletedTask);

        await ProductService.CreateProductAsync(product);

        ProductRepositoryMock.Verify(r => r.ExistsSkuAsync(product.Sku), Times.Once);
        ProductRepositoryMock.Verify(r => r.ExistsSupplierAsync(product.SupplierId), Times.Once);
        ProductRepositoryMock.Verify(r => r.AddProductsAsync(product), Times.Once);
    }

    [Fact]
    public async Task CreateProductAsync_WhenSkuAlreadyExists_ShouldThrowDomainException()
    {
        var product = new ProductBuilder()
            .WithValidData()
            .Build();

        ProductRepositoryMock
            .Setup(r => r.ExistsSkuAsync(product.Sku))
            .ReturnsAsync(true);

        await Assert.ThrowsAsync<DomainException>(() => ProductService.CreateProductAsync(product));

        ProductRepositoryMock.Verify(r => r.ExistsSkuAsync(product.Sku), Times.Once);
        ProductRepositoryMock.Verify(r => r.ExistsSupplierAsync(product.SupplierId), Times.Never);
        ProductRepositoryMock.Verify(r => r.AddProductsAsync(product), Times.Never);
    }

    [Fact]
    public async Task CreateProductAsync_WhenSupplierDoesNotExist_ShouldThrowNotFoundException()
    {
        var product = new ProductBuilder()
            .WithValidData()
            .Build();

        ProductRepositoryMock
            .Setup(r => r.ExistsSkuAsync(product.Sku))
            .ReturnsAsync(false);

        ProductRepositoryMock
            .Setup(r => r.ExistsSupplierAsync(product.SupplierId))
            .ReturnsAsync(false);

        await Assert.ThrowsAsync<NotFoundException>(() => ProductService.CreateProductAsync(product));

        ProductRepositoryMock.Verify(r => r.ExistsSkuAsync(product.Sku), Times.Once);
        ProductRepositoryMock.Verify(r => r.ExistsSupplierAsync(product.SupplierId), Times.Once);
        ProductRepositoryMock.Verify(r => r.AddProductsAsync(product), Times.Never);
    }

    [Fact]
    public async Task CreateProductAsync_WhenProductIsNull_ShouldThrowArgumentNullException()
    {
        await Assert.ThrowsAsync<ArgumentNullException>(() =>
            ProductService.CreateProductAsync(null!));

        ProductRepositoryMock.Verify(r => r.AddProductsAsync(It.IsAny<Product>()), Times.Never);
    }

    [Fact]
    public async Task GetByIdAsync_WhenProductExists_ShouldReturnProduct()
    {
        var product = new ProductBuilder()
            .WithValidData()
            .Build();

        ProductRepositoryMock
            .Setup(r => r.GetByIdAsync(product.Id))
            .ReturnsAsync(product);

        var result = await ProductService.GetByIdAsync(product.Id);

        Assert.NotNull(result);
        Assert.Equal(product, result);

        ProductRepositoryMock.Verify(r => r.GetByIdAsync(product.Id), Times.Once);
    }

    [Fact]
    public async Task GetByIdAsync_WhenProductDoesNotExist_ShouldThrowNotFoundException()
    {
        const int nonExistentProductId = 99;

        ProductRepositoryMock
            .Setup(r => r.GetByIdAsync(nonExistentProductId))
            .ReturnsAsync((Product?)null);

        await Assert.ThrowsAsync<NotFoundException>(() => ProductService.GetByIdAsync(nonExistentProductId));

        ProductRepositoryMock.Verify(r => r.GetByIdAsync(nonExistentProductId), Times.Once);
    }

    [Fact]
    public async Task UpdateProductAsync_WhenProductIsValid_ShouldUpdateSuccessfully()
    {
        var product = new ProductBuilder()
            .WithValidData()
            .Build();

        ProductRepositoryMock
            .Setup(r => r.GetByIdAsync(product.Id))
            .ReturnsAsync(product);

        ProductRepositoryMock
            .Setup(r => r.ExistsSkuAsync(product.Sku, product.Id))
            .ReturnsAsync(false);

        ProductRepositoryMock
            .Setup(r => r.ExistsSupplierAsync(product.SupplierId))
            .ReturnsAsync(true);

        ProductRepositoryMock
            .Setup(r => r.UpdateProductsAsync(product))
            .Returns(Task.CompletedTask);

        await ProductService.UpdateProductsAsync(product);

        ProductRepositoryMock.Verify(r => r.GetByIdAsync(product.Id), Times.Once);
        ProductRepositoryMock.Verify(r => r.ExistsSkuAsync(product.Sku, product.Id), Times.Once);
        ProductRepositoryMock.Verify(r => r.ExistsSupplierAsync(product.SupplierId), Times.Once);
        ProductRepositoryMock.Verify(r => r.UpdateProductsAsync(product), Times.Once);
    }

    [Fact]
    public async Task UpdateProductAsync_WhenRepositoryFailsToUpdate_ShouldThrowDomainException()
    {
        var product = new ProductBuilder()
            .WithValidData()
            .Build();

        ProductRepositoryMock
            .Setup(r => r.GetByIdAsync(product.Id))
            .ReturnsAsync(product);

        ProductRepositoryMock
            .Setup(r => r.ExistsSkuAsync(product.Sku, product.Id))
            .ReturnsAsync(false);

        ProductRepositoryMock
            .Setup(r => r.ExistsSupplierAsync(product.SupplierId))
            .ReturnsAsync(true);

        ProductRepositoryMock
            .Setup(r => r.UpdateProductsAsync(product))
            .ThrowsAsync(new DomainException("Could not update the product."));

        await Assert.ThrowsAsync<DomainException>(() => ProductService.UpdateProductsAsync(product));

        ProductRepositoryMock.Verify(r => r.GetByIdAsync(product.Id), Times.Once);
        ProductRepositoryMock.Verify(r => r.ExistsSkuAsync(product.Sku, product.Id), Times.Once);
        ProductRepositoryMock.Verify(r => r.ExistsSupplierAsync(product.SupplierId), Times.Once);
        ProductRepositoryMock.Verify(r => r.UpdateProductsAsync(product), Times.Once);
    }

    [Fact]
    public async Task RemoveProductAsync_WhenProductExistsAndHasNoStock_ShouldRemoveSuccessfully()
    {
        var product = new ProductBuilder()
            .WithValidData()
            .WithQuantity(0)
            .Build();

        ProductRepositoryMock
            .Setup(r => r.GetByIdAsync(product.Id))
            .ReturnsAsync(product);

        ProductRepositoryMock
            .Setup(r => r.RemoveProductsAsync(product))
            .Returns(Task.CompletedTask);

        await ProductService.RemoveProductsAsync(product.Id);

        ProductRepositoryMock.Verify(r => r.GetByIdAsync(product.Id), Times.Once);
        ProductRepositoryMock.Verify(r => r.RemoveProductsAsync(product), Times.Once);
    }

    [Fact]
    public async Task RemoveProductAsync_WhenProductHasStock_ShouldThrowDomainException()
    {
        var product = new ProductBuilder()
            .WithValidData()
            .Build();

        ProductRepositoryMock
            .Setup(r => r.GetByIdAsync(product.Id))
            .ReturnsAsync(product);

        await Assert.ThrowsAsync<DomainException>(() => ProductService.RemoveProductsAsync(product.Id));

        ProductRepositoryMock.Verify(r => r.GetByIdAsync(product.Id), Times.Once);
        ProductRepositoryMock.Verify(r => r.RemoveProductsAsync(product), Times.Never);
    }

    [Fact]
    public async Task RemoveProductAsync_WhenRepositoryFailsToRemove_ShouldThrowDomainException()
    {
        var product = new ProductBuilder()
            .WithValidData()
            .WithQuantity(0)
            .Build();

        ProductRepositoryMock
            .Setup(r => r.GetByIdAsync(product.Id))
            .ReturnsAsync(product);

        ProductRepositoryMock
            .Setup(r => r.RemoveProductsAsync(product))
            .ThrowsAsync(new DomainException("Could not remove the product."));

        await Assert.ThrowsAsync<DomainException>(() => ProductService.RemoveProductsAsync(product.Id));

        ProductRepositoryMock.Verify(r => r.GetByIdAsync(product.Id), Times.Once);
        ProductRepositoryMock.Verify(r => r.RemoveProductsAsync(product), Times.Once);
    }

    [Fact]
    public async Task ListProductsAsync_ShouldReturnProducts()
    {
        var products = new List<Product>
        {
            new ProductBuilder()
                .WithValidData()
                .WithId(1)
                .Build(),

            new ProductBuilder()
                .WithValidData()
                .WithId(2)
                .Build()
        };

        ProductRepositoryMock
            .Setup(r => r.ListProductsAsync())
            .ReturnsAsync(products);

        var result = await ProductService.ListProductsAsync();

        Assert.NotNull(result);
        Assert.Equal(2, result.Count);
        Assert.Equal(products, result);

        ProductRepositoryMock.Verify(r => r.ListProductsAsync(), Times.Once);
    }
}
