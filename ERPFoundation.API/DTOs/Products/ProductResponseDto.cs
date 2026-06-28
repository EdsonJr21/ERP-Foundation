namespace ERPFoundation.API.DTOs.Products;

public record ProductResponseDto(
    int Id,
    string Name,
    string Sku,
    decimal Price,
    int Quantity,
    int SupplierId
);