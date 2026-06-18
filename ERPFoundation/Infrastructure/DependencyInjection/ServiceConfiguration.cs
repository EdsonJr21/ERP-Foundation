using ERPFoundation.Application.Services;
using ERPFoundation.Application.Services.Interfaces;
using ERPFoundation.Infrastructure.Data;
using ERPFoundation.Infrastructure.Repositories;
using ERPFoundation.Infrastructure.Repositories.Interfaces;
using ERPFoundation.Presentation.ConsoleUI;
using Microsoft.Extensions.DependencyInjection;

namespace ERPFoundation.Infrastructure.DependencyInjection;

public static class ServiceConfiguration
{
    public static IServiceProvider Configure()
    {
        var services = new ServiceCollection();

        services.AddScoped<AppDbContext>();

        services.AddScoped<IProdutoRepository, ProdutoRepository>();
        services.AddScoped<IProdutoService, ProdutoService>();
        services.AddScoped<IFornecedorRepository, FornecedorRepository>();
        services.AddScoped<IFornecedorService, FornecedorService>();

        services.AddScoped<MainMenu>();
        services.AddScoped<ProdutoMenu>();
        services.AddScoped<FornecedorMenu>();

        return services.BuildServiceProvider();
    }
}
