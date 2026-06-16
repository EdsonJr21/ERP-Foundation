using ERPFoundation.Domain.Models;
using ERPFoundation.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace ERPFoundation.Infrastructure.Repositories;

public class FornecedorRepository
{
    private readonly AppDbContext _context;

    public FornecedorRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<bool> AdicionarFornecedorAsync(Fornecedor fornecedor)
    {
        ArgumentNullException.ThrowIfNull(fornecedor);

        await _context.Fornecedores.AddAsync(fornecedor);

        return await _context.SaveChangesAsync() > 0;
    }

    public async Task<List<Fornecedor>> ListarFornecedoresAsync()
    {
        return await _context.Fornecedores.ToListAsync();
    }

    public async Task<Fornecedor?> BuscarPorIdAsync(int id)
    {
        return await _context.Fornecedores
            .FirstOrDefaultAsync(f => f.Id == id);
    }

    public async Task<Fornecedor?> BuscaPorNomeAsync(string nome)
    {
        return await _context.Fornecedores
            .FirstOrDefaultAsync(f => f.Nome == nome);
    }
    
    public async Task<Fornecedor?> BuscaPorCnpjAsync(string cnpj)
    {
        return await _context.Fornecedores
            .FirstOrDefaultAsync(f => f.Cnpj == cnpj);
    }

    public async Task<bool> AtualizarFornecedorAsync(Fornecedor fornecedor)
    {
        _context.Fornecedores.Update(fornecedor);

        return await _context.SaveChangesAsync() > 0;
    }

    public async Task<bool> RemoverFornecedorAsync(Fornecedor fornecedor)
    {
        _context.Fornecedores.Remove(fornecedor);

        return await _context.SaveChangesAsync() > 0;
    }
}