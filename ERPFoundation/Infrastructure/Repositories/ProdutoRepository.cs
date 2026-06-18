using ERPFoundation.Domain.Models;
using ERPFoundation.Infrastructure.Data;
using ERPFoundation.Infrastructure.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ERPFoundation.Infrastructure.Repositories;

public class ProdutoRepository : IProdutoRepository
{
    private readonly AppDbContext _context;

    public ProdutoRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<bool> AdicionarProdutosAsync(Produto produto)
    {
        if (produto == null) throw new ArgumentNullException(nameof(produto));

        try
        {
            _context.Produtos.Add(produto);
            await _context.SaveChangesAsync();
            return true;
        }
        catch (DbUpdateException ex)
        {
            throw new ApplicationException("Erro ao adicionar produto (DB).", ex);
        }
        catch (Exception ex)
        {
            throw new ApplicationException("Erro ao adicionar produto.", ex);
        }
    }

    public async Task<List<Produto>> ListarProdutosAsync()
    {
        try
        {
            return await ApplyDefaultOrdering(_context.Produtos).ToListAsync();
        }
        catch (Exception ex)
        {
            throw new ApplicationException("Erro ao listar produtos.", ex);
        }
    }

    public async Task<List<Produto>> BuscarProdutosAsync(string nome)
    {
        try
        {
            var query = _context.Produtos.AsQueryable();

            if (!string.IsNullOrWhiteSpace(nome))
            {
                var termo = nome.Trim().ToLower();
                query = query.Where(p => p.Nome.ToLower().Contains(termo) || p.Sku.ToLower().Contains(termo));
            }

            return await ApplyDefaultOrdering(query).ToListAsync();
        }
        catch (Exception ex)
        {
            throw new ApplicationException("Erro ao buscar produtos.", ex);
        }
    }

    public async Task<bool> AtualizarProdutosAsync(int id, string nome, decimal preco, int quantidade)
    {
        try
        {
            var produto = await _context.Produtos.FindAsync(id);
            if (produto == null)
            {
                return false;
            }

            produto.Nome = nome;
            produto.Preco = preco;
            produto.Quantidade = quantidade;

            await _context.SaveChangesAsync();
            return true;
        }
        catch (DbUpdateException ex)
        {
            throw new ApplicationException("Erro ao atualizar produto (DB).", ex);
        }
        catch (Exception ex)
        {
            throw new ApplicationException("Erro ao atualizar produto.", ex);
        }
    }

    public async Task<Produto?> ObterPorIdAsync(int id)
    {
        return await _context.Produtos.FindAsync(id);
    }

    public async Task<bool> RemoverProdutosAsync(Produto produto)
    {
        if (produto == null) throw new ArgumentNullException(nameof(produto));

        try
        {
            _context.Produtos.Remove(produto);
            await _context.SaveChangesAsync();
            return true;
        }
        catch (DbUpdateException ex)
        {
            throw new ApplicationException("Erro ao remover produto (DB).", ex);
        }
        catch (Exception ex)
        {
            throw new ApplicationException("Erro ao remover produto.", ex);
        }
    }

    private static IQueryable<Produto> ApplyDefaultOrdering(IQueryable<Produto> query)
    {
        return query
            .OrderByDescending(p => p.Quantidade > 0)
            .ThenBy(p => p.Nome)
            .ThenBy(p => p.Preco);
    }

    public async Task<bool> ExisteSkuAsync(string sku)
    {
        if (string.IsNullOrWhiteSpace(sku)) return false;

        try
        {
            var skuNormalized = sku.Trim().ToUpperInvariant();
            return await _context.Produtos.AnyAsync(p => EF.Functions.Like(p.Sku, skuNormalized));
        }
        catch (Exception ex)
        {
            throw new ApplicationException(
                "Erro ao verificar SKU.",
                ex);
        }
    }

    public async Task<bool> ExisteFornecedorAsync(int fornecedorId)
    {
        if (fornecedorId <= 0) return false;

        try
        {
            return await _context.Fornecedores.AnyAsync(f => f.Id == fornecedorId);
        }
        catch (Exception ex)
        {
            throw new ApplicationException(
                "Erro ao verificar fornecedor.",
                ex);
        }
    }
}
