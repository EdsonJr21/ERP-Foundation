namespace ERP;

public interface IProdutoService
{
    Task<bool> CadastrarProdutoAsync(
        string sku,
        string nome,
        double preco,
        int quantidade
    );

    public Task<List<Produto>> ListarProdutosAsync();
    public Task<List<Produto>> BuscarProdutosAsync(string nome);
    public Task<bool> AtualizarProdutosAsync(int id, string nome, double preco, int quantidade);
    public Task<bool> RemoverProdutosAsync(int id);
}