using ERPFoundation.Domain.Models;

namespace ERPFoundation.Application.Services.Interfaces;

public interface IProdutoService
{
    Task<bool> CadastrarProdutoAsync(
        string sku,
        string nome,
        decimal preco,
        int quantidade,
        int fornecedorId
    );

    Task<(bool Cadastrado, string? Motivo)> CadastrarProdutoComDiagnosticoAsync(
        string sku,
        string nome,
        decimal preco,
        int quantidade,
        int fornecedorId
    );

    public Task<List<Produto>> ListarProdutosAsync();
    public Task<List<Produto>> BuscarProdutosAsync(string nome);
    public Task<bool> AtualizarProdutosAsync(int id, string nome, decimal preco, int quantidade);
    public Task<bool> RemoverProdutosAsync(int id);
}
