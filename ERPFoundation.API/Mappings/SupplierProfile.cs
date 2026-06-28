using AutoMapper;
using ERPFoundation.API.DTOs.Suppliers;
using ERPFoundation.Domain.Models;

namespace ERPFoundation.API.Mappings;

public class SupplierProfile : Profile
{
    public SupplierProfile()
    {
        CreateMap<CreateSupplierDto, Supplier>();

        CreateMap<UpdateSupplierDto, Supplier>();

        CreateMap<Supplier, SupplierResponseDto>();
    }
}
