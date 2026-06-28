namespace ERPFoundation.API.DTOs.Suppliers;

public record SupplierResponseDto(
    int Id,
    string Name,
    string TaxId,
    string Address
);