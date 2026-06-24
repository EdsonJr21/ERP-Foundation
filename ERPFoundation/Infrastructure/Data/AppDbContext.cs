using ERPFoundation.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace ERPFoundation.Infrastructure.Data;

public class AppDbContext : DbContext
{
    public DbSet<Product> Products { get; set; }
    public DbSet<Supplier> Suppliers { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        // Adjust the local password before running the application.
        const string connectionString = "Server=localhost;Database=erpfoundation;User=root;Password=;";

        optionsBuilder.UseMySql(
            connectionString,
            ServerVersion.AutoDetect(connectionString));
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(
            typeof(AppDbContext).Assembly);

        base.OnModelCreating(modelBuilder);
    }
}
