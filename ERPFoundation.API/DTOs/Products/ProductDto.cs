namespace ERPFoundation.API.DTOs.Products;

public record ProductDto(
    int Id,
    string Name,
    string Sku,
    decimal Price,
    int Quantity,
    int SupplierId
);