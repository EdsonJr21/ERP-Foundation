namespace ERPFoundation.API.DTOs.Suppliers;

public record CreateSupplierDto(
    string Name,
    string TaxId,
    string Address
);