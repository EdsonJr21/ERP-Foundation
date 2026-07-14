using ERPFoundation.Domain.Models;
using ERPFoundation.Tests.Builders;
using Moq;

namespace ERPFoundation.Tests.Application.Services.Suppliers;

public class SupplierServiceTests : SupplierServiceTestsBase
{
    [Fact]
    public async Task AddSupplierAsync_WhenSupplierIsValid_ShouldReturnTrue()
    {
        var supplier = new SupplierBuilder()
            .WithValidData()
            .Build();

        SupplierRepositoryMock
            .Setup(r => r.GetByTaxIdAsync(supplier.TaxId))
            .ReturnsAsync((Supplier?)null);

        SupplierRepositoryMock
            .Setup(r => r.AddSupplierAsync(supplier))
            .ReturnsAsync(true);

        var result = await SupplierService.AddSupplierAsync(supplier);

        Assert.True(result);

        SupplierRepositoryMock.Verify(
            r => r.AddSupplierAsync(supplier),
            Times.Once);
    }

    [Fact]
    public async Task AddSupplierAsync_WhenSupplierHasSpaces_ShouldNormalizeSupplierAndReturnTrue()
    {
        var supplier = new SupplierBuilder()
            .WithName("  Microsoft  ")
            .WithTaxId("  12345678901234  ")
            .WithAddress("  Redmond  ")
            .Build();

        SupplierRepositoryMock
            .Setup(r => r.GetByTaxIdAsync("12345678901234"))
            .ReturnsAsync((Supplier?)null);

        SupplierRepositoryMock
            .Setup(r => r.AddSupplierAsync(supplier))
            .ReturnsAsync(true);

        var result = await SupplierService.AddSupplierAsync(supplier);

        Assert.True(result);
        
        SupplierRepositoryMock.Verify(r =>
                r.AddSupplierAsync(It.Is<Supplier>(s =>
                    s.Name == "Microsoft" &&
                    s.TaxId == "12345678901234" &&
                    s.Address == "Redmond"
                )),
            Times.Once);
    }


    [Fact]
    public async Task AddSupplierAsync_WhenTaxIdAlreadyExists_ShouldReturnFalse()
    {
        var supplier = new SupplierBuilder()
            .WithValidData()
            .Build();

        SupplierRepositoryMock
            .Setup(r => r.GetByTaxIdAsync(supplier.TaxId))
            .ReturnsAsync(supplier);

        var result = await SupplierService.AddSupplierAsync(supplier);

        Assert.False(result);

        SupplierRepositoryMock.Verify(
            r => r.AddSupplierAsync(It.IsAny<Supplier>()),
            Times.Never);
    }


    [Fact]
    public async Task ListSuppliersAsync_WhenSuppliersExist_ShouldReturnSupplierList()
    {
        var suppliers = new List<Supplier>
        {
            new SupplierBuilder()
                .WithValidData()
                .WithTaxId("11111111111111")
                .Build(),

            new SupplierBuilder()
                .WithValidData()
                .WithTaxId("22222222222222")
                .Build()
        };

        SupplierRepositoryMock
            .Setup(r => r.ListSuppliersAsync())
            .ReturnsAsync(suppliers);

        var result = await SupplierService.ListSuppliersAsync();

        Assert.NotNull(result);
        Assert.Equal(2, result.Count);
        Assert.Equal(suppliers, result);
    }


    [Fact]
    public async Task GetByIdAsync_WhenSupplierExists_ShouldReturnSupplier()
    {
        var supplier = new SupplierBuilder()
            .WithValidData()
            .Build();

        SupplierRepositoryMock
            .Setup(r => r.GetByIdAsync(supplier.Id))
            .ReturnsAsync(supplier);

        var result = await SupplierService.GetByIdAsync(supplier.Id);

        Assert.NotNull(result);
        Assert.Equal(supplier, result);
    }


    [Fact]
    public async Task GetByIdAsync_WhenIdIsInvalid_ShouldReturnNull()
    {
        var result = await SupplierService.GetByIdAsync(0);

        Assert.Null(result);
    }


    [Fact]
    public async Task UpdateSupplierAsync_WhenSupplierIsValid_ShouldReturnTrue()
    {
        var supplierUpdate = new SupplierBuilder()
            .WithValidData()
            .WithName("Updated Supplier")
            .WithTaxId("11111111111111")
            .WithAddress("Updated Address")
            .Build();

        var existingSupplier = new SupplierBuilder()
            .WithValidData()
            .WithName("Old Supplier")
            .WithTaxId("22222222222222")
            .WithAddress("Old Address")
            .Build();


        SupplierRepositoryMock
            .Setup(r => r.GetByTaxIdAsync(supplierUpdate.TaxId))
            .ReturnsAsync((Supplier?)null);

        SupplierRepositoryMock
            .Setup(r => r.GetByIdAsync(supplierUpdate.Id))
            .ReturnsAsync(existingSupplier);

        SupplierRepositoryMock
            .Setup(r => r.UpdateSupplierAsync(existingSupplier))
            .ReturnsAsync(true);

        var result = await SupplierService.UpdateSupplierAsync(supplierUpdate);

        Assert.True(result);
        Assert.Equal(supplierUpdate.Name, existingSupplier.Name);
        Assert.Equal(supplierUpdate.TaxId, existingSupplier.TaxId);
        Assert.Equal(supplierUpdate.Address, existingSupplier.Address);

        SupplierRepositoryMock.Verify(
            r => r.UpdateSupplierAsync(existingSupplier),
            Times.Once);
    }

    [Fact]
    public async Task UpdateSupplierAsync_WhenTaxIdBelongsToSameSupplier_ShouldReturnTrue()
    {
        var supplierUpdate = new SupplierBuilder()
            .WithValidData()
            .WithName("Updated Supplier")
            .Build();

        var existingSupplier = new SupplierBuilder()
            .WithValidData()
            .WithName("Old Supplier")
            .Build();

        SupplierRepositoryMock
            .Setup(r => r.GetByTaxIdAsync(supplierUpdate.TaxId))
            .ReturnsAsync(existingSupplier);

        SupplierRepositoryMock
            .Setup(r => r.GetByIdAsync(supplierUpdate.Id))
            .ReturnsAsync(existingSupplier);

        SupplierRepositoryMock
            .Setup(r => r.UpdateSupplierAsync(existingSupplier))
            .ReturnsAsync(true);

        var result = await SupplierService.UpdateSupplierAsync(supplierUpdate);

        Assert.True(result);
        Assert.Equal("Updated Supplier", existingSupplier.Name);
    }


    [Fact]
    public async Task UpdateSupplierAsync_WhenSupplierIdIsInvalid_ShouldReturnFalse()
    {
        var supplier = new SupplierBuilder()
            .WithValidData()
            .WithId(0)
            .Build();

        var result = await SupplierService.UpdateSupplierAsync(supplier);

        Assert.False(result);

        SupplierRepositoryMock.Verify(
            r => r.GetByIdAsync(It.IsAny<int>()),
            Times.Never);

        SupplierRepositoryMock.Verify(
            r => r.UpdateSupplierAsync(It.IsAny<Supplier>()),
            Times.Never);
    }


    [Fact]
    public async Task UpdateSupplierAsync_WhenTaxIdAlreadyExists_ShouldReturnFalse()
    {
        var supplier = new SupplierBuilder()
            .WithValidData()
            .Build();

        var existingSupplier = new SupplierBuilder()
            .WithValidData()
            .WithId(2)
            .WithTaxId(supplier.TaxId)
            .Build();

        SupplierRepositoryMock
            .Setup(r => r.GetByTaxIdAsync(supplier.TaxId))
            .ReturnsAsync(existingSupplier);

        var result = await SupplierService.UpdateSupplierAsync(supplier);

        Assert.False(result);

        SupplierRepositoryMock.Verify(
            r => r.GetByIdAsync(It.IsAny<int>()),
            Times.Never);

        SupplierRepositoryMock.Verify(
            r => r.UpdateSupplierAsync(It.IsAny<Supplier>()),
            Times.Never);
    }


    [Fact]
    public async Task UpdateSupplierAsync_WhenSupplierDoesNotExist_ShouldReturnFalse()
    {
        var supplier = new SupplierBuilder()
            .WithValidData()
            .Build();

        SupplierRepositoryMock
            .Setup(r => r.GetByTaxIdAsync(supplier.TaxId))
            .ReturnsAsync((Supplier?)null);

        SupplierRepositoryMock
            .Setup(r => r.GetByIdAsync(supplier.Id))
            .ReturnsAsync((Supplier?)null);

        var result = await SupplierService.UpdateSupplierAsync(supplier);

        Assert.False(result);

        SupplierRepositoryMock.Verify(
            r => r.UpdateSupplierAsync(It.IsAny<Supplier>()),
            Times.Never);
    }


    [Fact]
    public async Task RemoveSupplierAsync_WhenSupplierIsValid_ShouldReturnTrue()
    {
        var supplier = new SupplierBuilder()
            .WithValidData()
            .Build();

        SupplierRepositoryMock
            .Setup(r => r.GetByIdAsync(supplier.Id))
            .ReturnsAsync(supplier);

        SupplierRepositoryMock
            .Setup(r => r.RemoveSupplierAsync(supplier))
            .ReturnsAsync(true);

        var result = await SupplierService.RemoveSupplierAsync(supplier.Id);

        Assert.True(result);

        SupplierRepositoryMock.Verify(
            r => r.RemoveSupplierAsync(supplier),
            Times.Once);
    }


    [Fact]
    public async Task RemoveSupplierAsync_WhenSupplierIdIsInvalid_ShouldReturnFalse()
    {
        var result = await SupplierService.RemoveSupplierAsync(0);

        Assert.False(result);

        SupplierRepositoryMock.Verify(
            r => r.GetByIdAsync(It.IsAny<int>()),
            Times.Never);
    }


    [Fact]
    public async Task RemoveSupplierAsync_WhenSupplierDoesNotExist_ShouldReturnFalse()
    {
        SupplierRepositoryMock
            .Setup(r => r.GetByIdAsync(1))
            .ReturnsAsync((Supplier?)null);

        var result = await SupplierService.RemoveSupplierAsync(1);

        Assert.False(result);

        SupplierRepositoryMock.Verify(
            r => r.RemoveSupplierAsync(It.IsAny<Supplier>()),
            Times.Never);
    }
}