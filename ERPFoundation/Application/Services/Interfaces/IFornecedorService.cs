using ERPFoundation.Domain.Models;

namespace ERPFoundation.Application.Services.Interfaces;

public interface IFornecedorService
{
    Task<bool> AdicionarFornecedorAsync(Fornecedor fornecedor);
    Task<List<Fornecedor>> ListarFornecedoresAsync();
    Task<Fornecedor?> BuscarPorIdAsync(int id);
    Task<bool> AtualizarFornecedorAsync(Fornecedor fornecedor);
    Task<bool> RemoverFornecedorAsync(int id);
}
