using ERPFoundation.API.DTOs.Suppliers;
using ERPFoundation.Application.Services.Interfaces;
using ERPFoundation.Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace ERPFoundation.API.Controllers.Suppliers;

[ApiController]
[Route("api/suppliers")]
public class SuppliersController : ControllerBase
{
    private readonly ISupplierService _supplierService;

    public SuppliersController(ISupplierService supplierService)
    {
        _supplierService = supplierService;
    }

    [HttpGet]
    public async Task<IActionResult> List()
    {
        var suppliers = await _supplierService.ListSuppliersAsync();

        var suppliersDto = suppliers.Select(s => new SupplierDto(
            s.Id,
            s.Name,
            s.TaxId,
            s.Address
        ));

        return Ok(suppliersDto);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id)
    {
        var supplier = await _supplierService.GetByIdAsync(id);

        if (supplier is null)
        {
            return NotFound("Supplier not found.");
        }

        var supplierDto = new SupplierDto(
            supplier.Id,
            supplier.Name,
            supplier.TaxId,
            supplier.Address
        );

        return Ok(supplierDto);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateSupplierDto dto)
    {
        var supplier = new Supplier
        {
            Name = dto.Name,
            TaxId = dto.TaxId,
            Address = dto.Address
        };

        var result = await _supplierService.AddSupplierAsync(supplier);

        if (!result)
        {
            return BadRequest("Invalid data or supplier already registered.");
        }

        var supplierDto = new SupplierDto(
            supplier.Id,
            supplier.Name,
            supplier.TaxId,
            supplier.Address
        );

        return CreatedAtAction(
            nameof(GetById),
            new { id = supplierDto.Id },
            supplierDto
        );
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, [FromBody] UpdateSupplierDto dto)
    {
        var existingSupplier = await _supplierService.GetByIdAsync(id);

        if (existingSupplier is null)
        {
            return NotFound("Supplier not found.");
        }

        existingSupplier.Name = dto.Name;
        existingSupplier.TaxId = dto.TaxId;
        existingSupplier.Address = dto.Address;

        var result = await _supplierService.UpdateSupplierAsync(existingSupplier);

        if (!result)
        {
            return BadRequest("Invalid data or Tax ID already registered.");
        }

        return NoContent();
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Remove(int id)
    {
        var existingSupplier = await _supplierService.GetByIdAsync(id);

        if (existingSupplier is null)
        {
            return NotFound("Supplier not found.");
        }

        var result = await _supplierService.RemoveSupplierAsync(id);

        if (!result)
        {
            return BadRequest("Could not remove the supplier.");
        }

        return NoContent();
    }
}
