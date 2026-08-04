using AutoMapper;
using ERPFoundation.API.DTOs.Suppliers;
using ERPFoundation.Application.Services.Interfaces;
using ERPFoundation.Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace ERPFoundation.API.Controllers.Suppliers;

[ApiController]
[Route("api/suppliers")]
public class SuppliersController(ISupplierService supplierService, IMapper mapper) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> List()
    {
        var suppliers = await supplierService.ListSuppliersAsync();

        var suppliersDto = mapper.Map<List<SupplierResponseDto>>(suppliers);

        return Ok(suppliersDto);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id)
    {
        var supplier = await supplierService.GetByIdAsync(id);

        var supplierDto = mapper.Map<SupplierResponseDto>(supplier);

        return Ok(supplierDto);
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateSupplierDto dto)
    {
        var supplier = mapper.Map<Supplier>(dto);

        await supplierService.AddSupplierAsync(supplier);

        var supplierDto = mapper.Map<SupplierResponseDto>(supplier);

        return CreatedAtAction(
            nameof(GetById),
            new { id = supplierDto.Id },
            supplierDto
        );
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, UpdateSupplierDto dto)
    {
        var existingSupplier = await supplierService.GetByIdAsync(id);

        mapper.Map(dto, existingSupplier);

        await supplierService.UpdateSupplierAsync(existingSupplier);

        return NoContent();
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Remove(int id)
    {
        await supplierService.RemoveSupplierAsync(id);

        return NoContent();
    }
}