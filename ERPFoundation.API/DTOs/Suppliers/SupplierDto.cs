namespace ERPFoundation.API.DTOs.Suppliers;

public record SupplierDto(
    int Id,
    string Name,
    string TaxId,
    string Address
);