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
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddScoped<AppDbContext>();

        services.AddScoped<IProductRepository, ProductRepository>();
        services.AddScoped<IProductService, ProductService>();

        services.AddScoped<ISupplierRepository, SupplierRepository>();
        services.AddScoped<ISupplierService, SupplierService>();

        return services;
    }

    public static IServiceProvider Configure()
    {
        var services = new ServiceCollection();

        services.AddInfrastructure();

        services.AddScoped<MainMenu>();
        services.AddScoped<ProductMenu>();
        services.AddScoped<SupplierMenu>();

        return services.BuildServiceProvider();
    }
}