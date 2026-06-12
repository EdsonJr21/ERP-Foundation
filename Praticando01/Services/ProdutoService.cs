using Praticando01.Models;
using Praticando01.Repositories;

namespace Praticando01.Services;

public class ProdutoService : IProdutoService
{
    private readonly IProdutoRepository _produtoRepository;

    public ProdutoService(IProdutoRepository produtoRepository)
    {
        ArgumentNullException.ThrowIfNull(produtoRepository);
        _produtoRepository = produtoRepository;
    }

    public async Task<bool> CadastrarProdutoAsync(
        string sku,
        string nome,
        double preco,
        int quantidade)
    {
        if (string.IsNullOrWhiteSpace(nome) || string.IsNullOrWhiteSpace(sku) || preco < 0 || quantidade < 0)
        {
            return false;
        }

        var skuNorm = sku.Trim().ToUpperInvariant();
        var nomeNorm = nome.Trim();

        if (await _produtoRepository.ExisteSkuAsync(skuNorm))
        {
            return false;
        }

        var produto = new Produto(skuNorm, nomeNorm, preco, quantidade);
        return await _produtoRepository.AdicionarProdutosAsync(produto);
    }


    public async Task<List<Produto>> ListarProdutosAsync()
    {
        return await _produtoRepository.ListarProdutosAsync();
    }

    public async Task<List<Produto>> BuscarProdutosAsync(string nome)
    {
        if (string.IsNullOrWhiteSpace(nome))
        {
            return new List<Produto>();
        }

        return await _produtoRepository.BuscarProdutosAsync(nome);
    }

    public async Task<bool> AtualizarProdutosAsync(
        int id,
        string nome,
        double preco,
        int quantidade)
    {
        if (id <= 0 || string.IsNullOrWhiteSpace(nome) || preco < 0 || quantidade < 0)
        {
            return false;
        }

        var nomeNorm = nome.Trim();
        return await _produtoRepository.AtualizarProdutosAsync(id, nomeNorm, preco, quantidade);
    }

    public async Task<bool> RemoverProdutosAsync(int id)
    {
        if (id <= 0) return false;

        var produto = await _produtoRepository.ObterPorIdAsync(id);

        if (produto == null) return false;
        if (produto.Quantidade > 0) return false;

        return await _produtoRepository.RemoverProdutosAsync(produto);
    }
}