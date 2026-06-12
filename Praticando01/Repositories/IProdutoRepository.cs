using Praticando01.Models;

namespace Praticando01.Repositories;

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
