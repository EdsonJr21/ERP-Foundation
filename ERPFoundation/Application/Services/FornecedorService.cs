using ERPFoundation.Domain.Models;
using ERPFoundation.Application.Services.Interfaces;
using ERPFoundation.Infrastructure.Repositories.Interfaces;

namespace ERPFoundation.Application.Services;

public class FornecedorService : IFornecedorService
{
    private readonly IFornecedorRepository _fornecedorRepository;

    public FornecedorService(IFornecedorRepository fornecedorRepository)
    {
        ArgumentNullException.ThrowIfNull(fornecedorRepository);
        _fornecedorRepository = fornecedorRepository;
    }

    public async Task<bool> AdicionarFornecedorAsync(Fornecedor fornecedor)
    {
        ArgumentNullException.ThrowIfNull(fornecedor);

        if (string.IsNullOrWhiteSpace(fornecedor.Nome) ||
            string.IsNullOrWhiteSpace(fornecedor.Cnpj) ||
            string.IsNullOrWhiteSpace(fornecedor.Endereco))
            return false;

        if (fornecedor.Nome.Trim().Length < 3 || fornecedor.Cnpj.Trim().Length != 14)
            return false;

        var fornecedorExistente =
            await _fornecedorRepository.BuscaPorCnpjAsync(fornecedor.Cnpj);

        if (fornecedorExistente != null)
            return false;

        return await _fornecedorRepository.AdicionarFornecedorAsync(fornecedor);
    }

    public async Task<List<Fornecedor>> ListarFornecedoresAsync()
    {
        return await _fornecedorRepository.ListarFornecedoresAsync();
    }

    public async Task<Fornecedor?> BuscarPorIdAsync(int id)
    {
        return await _fornecedorRepository.BuscarPorIdAsync(id);
    }

    public async Task<bool> AtualizarFornecedorAsync(Fornecedor fornecedor)
    {
        ArgumentNullException.ThrowIfNull(fornecedor);

        if (string.IsNullOrWhiteSpace(fornecedor.Nome) ||
            string.IsNullOrWhiteSpace(fornecedor.Cnpj) ||
            string.IsNullOrWhiteSpace(fornecedor.Endereco))
            return false;

        var fornecedorExistente = await _fornecedorRepository.BuscarPorIdAsync(fornecedor.Id);

        if (fornecedorExistente == null) return false;

        fornecedorExistente.Nome = fornecedor.Nome;
        fornecedorExistente.Cnpj = fornecedor.Cnpj;
        fornecedorExistente.Endereco = fornecedor.Endereco;

        return await _fornecedorRepository.AtualizarFornecedorAsync(fornecedorExistente);
    }

    public async Task<bool> RemoverFornecedorAsync(int id)
    {
        var fornecedor = await _fornecedorRepository.BuscarPorIdAsync(id);

        if (fornecedor == null) return false;

        return await _fornecedorRepository.RemoverFornecedorAsync(fornecedor);
    }
}
