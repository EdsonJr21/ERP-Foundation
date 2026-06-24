namespace ERPFoundation.API.DTOs.Suppliers;

public record UpdateSupplierDto(
    string Name,
    string TaxId,
    string Address
);