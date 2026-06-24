namespace ERPFoundation.API.DTOs.Products;

public record CreateProductDto(
    string Name,
    string Sku,
    decimal Price,
    int Quantity,
    int SupplierId
);