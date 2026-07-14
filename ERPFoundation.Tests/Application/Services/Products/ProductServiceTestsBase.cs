using ERPFoundation.Application.Services;
using ERPFoundation.Infrastructure.Repositories.Interfaces;
using Moq;

namespace ERPFoundation.Tests.Application.Services.Products;

public class ProductServiceTestsBase
{
    protected readonly Mock<IProductRepository> ProductRepositoryMock;
    protected readonly ProductService ProductService;

    protected ProductServiceTestsBase()
    {
        ProductRepositoryMock = new Mock<IProductRepository>();
        ProductService = new ProductService(ProductRepositoryMock.Object);
    }
}