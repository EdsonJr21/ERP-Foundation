using ERPFoundation.Application.Services;
using ERPFoundation.Infrastructure.Repositories.Interfaces;
using Moq;

namespace ERPFoundation.Tests.Application.Services.Suppliers;

public class SupplierServiceTestsBase
{
    protected readonly Mock<ISupplierRepository> SupplierRepositoryMock;
    protected readonly SupplierService SupplierService;

    public SupplierServiceTestsBase()
    {
        SupplierRepositoryMock = new Mock<ISupplierRepository>();
        SupplierService = new SupplierService(SupplierRepositoryMock.Object);
    }
}