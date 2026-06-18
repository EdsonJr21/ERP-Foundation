using ERPFoundation.Domain.Models;

namespace ERPFoundation.Infrastructure.Repositories.Interfaces;

public interface IFornecedorRepository
{
    Task<bool> AdicionarFornecedorAsync(Fornecedor fornecedor);
    Task<List<Fornecedor>> ListarFornecedoresAsync();
    Task<Fornecedor?> BuscarPorIdAsync(int id);
    Task<Fornecedor?> BuscaPorCnpjAsync(string cnpj);
    Task<bool> AtualizarFornecedorAsync(Fornecedor fornecedor);
    Task<bool> RemoverFornecedorAsync(Fornecedor fornecedor);
}
