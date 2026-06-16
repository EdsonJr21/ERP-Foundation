using ERPFoundation.Domain.Models;

namespace ERPFoundation.Infrastructure.Repositories.Interfaces;

public interface IProdutoRepository
{
    Task<bool> AdicionarProdutosAsync(Produto produto);
    Task<List<Produto>> ListarProdutosAsync();
    Task<List<Produto>> BuscarProdutosAsync(string nome);

    Task<bool> AtualizarProdutosAsync(
        int id,
        string novoNome,
        double novoPreco,
        int novaQuantidade);

    Task<Produto?> ObterPorIdAsync(int id);

    Task<bool> RemoverProdutosAsync(Produto produto);
    Task<bool> ExisteSkuAsync(string sku);
}
