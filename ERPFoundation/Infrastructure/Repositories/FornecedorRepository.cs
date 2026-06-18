using ERPFoundation.Domain.Models;
using ERPFoundation.Infrastructure.Data;
using ERPFoundation.Infrastructure.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ERPFoundation.Infrastructure.Repositories;

public class FornecedorRepository : IFornecedorRepository
{
    private readonly AppDbContext _context;

    public FornecedorRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<bool> AdicionarFornecedorAsync(Fornecedor fornecedor)
    {
        ArgumentNullException.ThrowIfNull(fornecedor);

        try
        {
            await _context.Fornecedores.AddAsync(fornecedor);

            return await _context.SaveChangesAsync() > 0;
        }
        catch (DbUpdateException ex)
        {
            throw new ApplicationException("Erro ao adicionar fornecedor (DB).", ex);
        }
        catch (Exception ex)
        {
            throw new ApplicationException("Erro ao adicionar fornecedor.", ex);
        }
    }

    public async Task<List<Fornecedor>> ListarFornecedoresAsync()
    {
        try
        {
            return await _context.Fornecedores.ToListAsync();
        }
        catch (Exception ex)
        {
            throw new ApplicationException("Erro ao listar fornecedores.", ex);
        }
    }

    public async Task<Fornecedor?> BuscarPorIdAsync(int id)
    {
        try
        {
            return await _context.Fornecedores
                .FirstOrDefaultAsync(f => f.Id == id);
        }
        catch (Exception ex)
        {
            throw new ApplicationException("Erro ao buscar fornecedor por ID.", ex);
        }
    }

    public async Task<Fornecedor?> BuscaPorCnpjAsync(string cnpj)
    {
        try
        {
            return await _context.Fornecedores
                .FirstOrDefaultAsync(f => f.Cnpj == cnpj);
        }
        catch (Exception ex)
        {
            throw new ApplicationException("Erro ao buscar fornecedor por CNPJ.", ex);
        }
    }

    public async Task<bool> AtualizarFornecedorAsync(Fornecedor fornecedor)
    {
        ArgumentNullException.ThrowIfNull(fornecedor);

        try
        {
            _context.Fornecedores.Update(fornecedor);

            return await _context.SaveChangesAsync() > 0;
        }
        catch (DbUpdateException ex)
        {
            throw new ApplicationException("Erro ao atualizar fornecedor (DB).", ex);
        }
        catch (Exception ex)
        {
            throw new ApplicationException("Erro ao atualizar fornecedor.", ex);
        }
    }

    public async Task<bool> RemoverFornecedorAsync(Fornecedor fornecedor)
    {
        ArgumentNullException.ThrowIfNull(fornecedor);

        try
        {
            _context.Fornecedores.Remove(fornecedor);

            return await _context.SaveChangesAsync() > 0;
        }
        catch (DbUpdateException ex)
        {
            throw new ApplicationException("Erro ao remover fornecedor (DB).", ex);
        }
        catch (Exception ex)
        {
            throw new ApplicationException("Erro ao remover fornecedor.", ex);
        }
    }
}