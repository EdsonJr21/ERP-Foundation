using ERPFoundation.Application.Services;
using ERPFoundation.Application.Services.Interfaces;
using ERPFoundation.Infrastructure.Data;
using ERPFoundation.Infrastructure.Repositories;
using ERPFoundation.Infrastructure.Repositories.Interfaces;
using ERPFoundation.Presentation.ConsoleUI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ERPFoundation.Infrastructure.DependencyInjection;

public static class ServiceConfiguration
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services,
        IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("DefaultConnection");

        services.AddDbContext<AppDbContext>(options =>
            options.UseMySql(
                connectionString,
                ServerVersion.AutoDetect(connectionString)
            ));

        services.AddScoped<IProductRepository, ProductRepository>();
        services.AddScoped<IProductService, ProductService>();

        services.AddScoped<ISupplierRepository, SupplierRepository>();
        services.AddScoped<ISupplierService, SupplierService>();

        return services;
    }

    public static IServiceProvider Configure()
    {
        var services = new ServiceCollection();

        const string connectionString = "Server=localhost;Database=erpfoundation;User=root;Password=;";

        services.AddDbContext<AppDbContext>(options =>
            options.UseMySql(
                connectionString,
                ServerVersion.AutoDetect(connectionString)));

        services.AddScoped<IProductRepository, ProductRepository>();
        services.AddScoped<IProductService, ProductService>();

        services.AddScoped<ISupplierRepository, SupplierRepository>();
        services.AddScoped<ISupplierService, SupplierService>();

        services.AddScoped<MainMenu>();
        services.AddScoped<ProductMenu>();
        services.AddScoped<SupplierMenu>();

        return services.BuildServiceProvider();
    }
}