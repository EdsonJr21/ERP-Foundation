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

        if (supplier is null)
        {
            throw new KeyNotFoundException("Supplier not found.");
        }

        var supplierDto = mapper.Map<SupplierResponseDto>(supplier);

        return Ok(supplierDto);
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateSupplierDto dto)
    {
        var supplier = mapper.Map<Supplier>(dto);

        var result = await supplierService.AddSupplierAsync(supplier);

        if (!result)
        {
            throw new ArgumentException("Invalid data or supplier already registered.");
        }

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

        if (existingSupplier is null)
        {
            throw new KeyNotFoundException("Supplier not found.");
        }

        mapper.Map(dto, existingSupplier);

        var result = await supplierService.UpdateSupplierAsync(existingSupplier);

        return !result ? throw new ArgumentException("Invalid data or Tax ID already registered.") : NoContent();
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Remove(int id)
    {
        var existingSupplier = await supplierService.GetByIdAsync(id);

        if (existingSupplier is null)
        {
            throw new KeyNotFoundException("Supplier not found.");
        }

        var result = await supplierService.RemoveSupplierAsync(id);

        return !result ? throw new ArgumentException("Could not remove the supplier.") : NoContent();
    }
}