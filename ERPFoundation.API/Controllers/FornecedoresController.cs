using ERPFoundation.API.DTOs;
using ERPFoundation.Application.Services.Interfaces;
using ERPFoundation.Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace ERPFoundation.API.Controllers;

[ApiController]
[Route("api/fornecedores")]
public class FornecedoresController : ControllerBase
{
    private readonly IFornecedorService _fornecedorService;

    public FornecedoresController(IFornecedorService fornecedorService)
    {
        _fornecedorService = fornecedorService;
    }

    [HttpGet]
    public async Task<IActionResult> Listar()
    {
        var fornecedores = await _fornecedorService.ListarFornecedoresAsync();

        return Ok(fornecedores);
    }

    [HttpPost]
    public async Task<IActionResult> Cadastrar([FromBody] CriarFornecedorDto dto)
    {
        var fornecedor = new Fornecedor()
        {
            Nome = dto.Nome,
            Cnpj = dto.Cnpj,
            Endereco = dto.Endereco
        };

        var resultado = await _fornecedorService.AdicionarFornecedorAsync(fornecedor);

        if (!resultado)
            return BadRequest("Dados inválidos ou fornecedor já cadastrado.");

        return Ok();
    }
}