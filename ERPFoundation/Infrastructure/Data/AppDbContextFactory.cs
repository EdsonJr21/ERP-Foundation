using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace ERPFoundation.Infrastructure.Data;

public class AppDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
{
    public AppDbContext CreateDbContext(string[] args)
    {
        var basePath = GetConfigurationBasePath();

        var configuration = new ConfigurationBuilder()
            .SetBasePath(basePath)
            .AddJsonFile("appsettings.json", optional: false)
            .AddJsonFile("appsettings.Development.json", optional: true)
            .Build();

        var connectionString = configuration.GetConnectionString("DefaultConnection")
                               ?? throw new InvalidOperationException(
                                   "Connection string 'DefaultConnection' not found.");

        var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();
        
        optionsBuilder.UseMySql(
            connectionString,
            ServerVersion.AutoDetect(connectionString));

        return new AppDbContext(optionsBuilder.Options);
    }

    private static string GetConfigurationBasePath()
    {
        var currentDirectory = Directory.GetCurrentDirectory();
        var candidatePaths = new[]
        {
            currentDirectory,
            Path.Combine(currentDirectory, "ERPFoundation.API"),
            Path.GetFullPath(Path.Combine(currentDirectory, "..", "ERPFoundation.API"))
        };

        return candidatePaths.FirstOrDefault(path =>
                   File.Exists(Path.Combine(path, "appsettings.json")))
               ?? throw new DirectoryNotFoundException(
                   "Could not locate the API appsettings.json file for the design-time DbContext.");
    }
}
