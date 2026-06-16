using ERPFoundation.Application.Services;
using ERPFoundation.Application.Services.Interfaces;
using ERPFoundation.Infrastructure.Data;
using ERPFoundation.Infrastructure.Repositories;
using ERPFoundation.Infrastructure.Repositories.Interfaces;

namespace ERPFoundation.Application.Configuration;

public class ServiceConfigurator
{
    public static IProdutoService Configurar(AppDbContext context)
    {
        IProdutoRepository repository = new ProdutoRepository(context);
        return new ProdutoService(repository);
    }
}