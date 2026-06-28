using AutoMapper;
using ERPFoundation.API.DTOs.Products;
using ERPFoundation.Domain.Models;

namespace ERPFoundation.API.Mappings;

public class ProductProfile : Profile
{
    public ProductProfile()
    {
        CreateMap<CreateProductDto, Product>();

        CreateMap<UpdateProductDto, Product>();

        CreateMap<Product, ProductResponseDto>();
    }
}