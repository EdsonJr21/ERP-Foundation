using Praticando01.Data;
using Praticando01.Repositories;

namespace Praticando01.Services;

public class ServiceConfigurator
{
    public static IProdutoService Configurar(AppDbContext context)
    {
        IProdutoRepository repository = new ProdutoRepository(context);
        return new ProdutoService(repository);
    }
}