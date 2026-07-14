namespace ERPFoundation.API.DTOs.Products;

public record UpdateProductDto(
    string Name,
    decimal Price,
    int Quantity,
    int SupplierId
);