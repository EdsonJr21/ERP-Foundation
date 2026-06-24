namespace ERPFoundation.API.DTOs.Products;

public record UpdateProductDto(
    string Name,
    string Sku,
    decimal Price,
    int Quantity,
    int SupplierId
);