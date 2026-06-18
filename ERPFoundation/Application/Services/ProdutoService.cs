using ERPFoundation.Application.Services.Interfaces;
using ERPFoundation.Domain.Models;
using ERPFoundation.Infrastructure.Repositories.Interfaces;

namespace ERPFoundation.Application.Services;

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
        decimal preco,
        int quantidade,
        int fornecedorId)
    {
        var resultado = await CadastrarProdutoComDiagnosticoAsync(
            sku,
            nome,
            preco,
            quantidade,
            fornecedorId);

        return resultado.Cadastrado;
    }

    public async Task<(bool Cadastrado, string? Motivo)> CadastrarProdutoComDiagnosticoAsync(
        string sku,
        string nome,
        decimal preco,
        int quantidade,
        int fornecedorId)
    {
        if (string.IsNullOrWhiteSpace(nome) ||
            string.IsNullOrWhiteSpace(sku) ||
            quantidade < 0 ||
            fornecedorId <= 0)
        {
            return (false, "Dados invalidos: SKU e nome sao obrigatorios, quantidade nao pode ser negativa e fornecedor deve ser maior que zero.");
        }

        if (preco <= 0)
        {
            return (false, "Dados invalidos: preco deve ser maior que zero.");
        }

        var skuNorm = sku.Trim().ToUpperInvariant();
        var nomeNorm = nome.Trim();

        if (await _produtoRepository.ExisteSkuAsync(skuNorm))
        {
            return (false, $"SKU '{skuNorm}' ja esta cadastrado.");
        }

        if (!await _produtoRepository.ExisteFornecedorAsync(fornecedorId))
        {
            return (false, $"Fornecedor com ID {fornecedorId} nao existe.");
        }

        var produto = new Produto(skuNorm, nomeNorm, preco, quantidade, fornecedorId);
        var cadastrado = await _produtoRepository.AdicionarProdutosAsync(produto);

        return cadastrado
            ? (true, null)
            : (false, "Repositorio retornou falso ao salvar o produto.");
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
        decimal preco,
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
