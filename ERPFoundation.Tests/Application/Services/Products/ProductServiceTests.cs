using ERPFoundation.Domain.Models;
using ERPFoundation.Tests.Builders;
using Moq;

namespace ERPFoundation.Tests.Application.Services.Products;

public class ProductServiceTests : ProductServiceTestsBase
{
    [Fact]
    public async Task CreateProductAsync_WhenProductIsValid_ShouldReturnTrue()
    {
        var product = new ProductBuilder()
            .WithValidData()
            .Build();

        ProductRepositoryMock
            .Setup(r => r.ExistsSkuAsync(product.Sku, null))
            .ReturnsAsync(false);

        ProductRepositoryMock
            .Setup(r => r.ExistsSupplierAsync(product.SupplierId))
            .ReturnsAsync(true);

        ProductRepositoryMock
            .Setup(r => r.AddProductsAsync(product))
            .ReturnsAsync(true);
        
        var result = await ProductService.CreateProductAsync(product);
        
        Assert.True(result);

        ProductRepositoryMock.Verify(r => r.ExistsSkuAsync(product.Sku, null), Times.Once);
        ProductRepositoryMock.Verify(r => r.ExistsSupplierAsync(product.SupplierId), Times.Once);
        ProductRepositoryMock.Verify(r => r.AddProductsAsync(product), Times.Once);
    }

    [Fact]
    public async Task CreateProductAsync_WhenSkuAlreadyExists_ShouldReturnFalse()
    {
        var product = new ProductBuilder()
            .WithValidData()
            .Build();

        ProductRepositoryMock
            .Setup(r => r.ExistsSkuAsync(product.Sku, null))
            .ReturnsAsync(true);
        
        var result = await ProductService.CreateProductAsync(product);
        
        Assert.False(result);

        ProductRepositoryMock.Verify(r => r.ExistsSkuAsync(product.Sku, null), Times.Once);
        ProductRepositoryMock.Verify(r => r.ExistsSupplierAsync(product.SupplierId), Times.Never);
        ProductRepositoryMock.Verify(r => r.AddProductsAsync(product), Times.Never);
    }

    [Fact]
    public async Task CreateProductAsync_WhenSupplierDoesNotExist_ShouldReturnFalse()
    {
        var product = new ProductBuilder()
            .WithValidData()
            .Build();

        ProductRepositoryMock
            .Setup(r => r.ExistsSkuAsync(product.Sku, null))
            .ReturnsAsync(false);

        ProductRepositoryMock
            .Setup(r => r.ExistsSupplierAsync(product.SupplierId))
            .ReturnsAsync(false);
        
        var result = await ProductService.CreateProductAsync(product);
        
        Assert.False(result);

        ProductRepositoryMock.Verify(r => r.ExistsSkuAsync(product.Sku, null), Times.Once);
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
        
        var result = await ProductService.GetByIdAsync(1);
        
        Assert.NotNull(result);
        Assert.Equal(product, result);

        ProductRepositoryMock.Verify(r => r.GetByIdAsync(product.Id), Times.Once);
    }

    [Fact]
    public async Task GetByIdAsync_WhenProductDoesNotExist_ShouldReturnNull()
    {
        const int nonExistentProductId = 99;

        ProductRepositoryMock
            .Setup(r => r.GetByIdAsync(nonExistentProductId))
            .ReturnsAsync((Product?)null);
        
        var result = await ProductService.GetByIdAsync(nonExistentProductId);
        
        Assert.Null(result);

        ProductRepositoryMock.Verify(r => r.GetByIdAsync(nonExistentProductId), Times.Once);
    }

    [Fact]
    public async Task UpdateProductAsync_WhenProductIsValid_ShouldReturnTrue()
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
            .ReturnsAsync(true);
        
        var result = await ProductService.UpdateProductsAsync(product);
        
        Assert.True(result);

        ProductRepositoryMock.Verify(r => r.GetByIdAsync(product.Id), Times.Once);
        ProductRepositoryMock.Verify(r => r.ExistsSkuAsync(product.Sku, product.Id), Times.Once);
        ProductRepositoryMock.Verify(r => r.ExistsSupplierAsync(product.SupplierId), Times.Once);
        ProductRepositoryMock.Verify(r => r.UpdateProductsAsync(product), Times.Once);
    }
    
    [Fact]
    public async Task RemoveProductAsync_WhenProductExistsAndHasNoStock_ShouldReturnTrue()
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
            .ReturnsAsync(true);
        
        var result = await ProductService.RemoveProductsAsync(product.Id);
        
        Assert.True(result);

        ProductRepositoryMock.Verify(r => r.GetByIdAsync(product.Id), Times.Once);
        ProductRepositoryMock.Verify(r => r.RemoveProductsAsync(product), Times.Once);
    }

    [Fact]
    public async Task RemoveProductAsync_WhenProductHasStock_ShouldReturnFalse()
    {
        var product = new ProductBuilder()
            .WithValidData()
            .Build();

        ProductRepositoryMock
            .Setup(r => r.GetByIdAsync(product.Id))
            .ReturnsAsync(product);
        
        var result = await ProductService.RemoveProductsAsync(product.Id);
        
        Assert.False(result);

        ProductRepositoryMock.Verify(r => r.GetByIdAsync(product.Id), Times.Once);
        ProductRepositoryMock.Verify(r => r.RemoveProductsAsync(product), Times.Never);
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
