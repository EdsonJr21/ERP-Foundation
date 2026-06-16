using ERPFoundation.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace ERPFoundation.Infrastructure.Data;

public class AppDbContext : DbContext
{
    public DbSet<Produto> Produtos { get; set; }
    public DbSet<Fornecedor> Fornecedores { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        // quando for usar tem que ajustar a senha localmente antes de executar
        string connectionString = "Server=localhost;Database=erpfoundation;User=root;Password=;";

        optionsBuilder.UseMySql(
            connectionString,
            ServerVersion.AutoDetect(connectionString));
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Garante a unicidade de CNPJ no nível do banco
        modelBuilder.Entity<Fornecedor>()
            .HasIndex(f => f.Cnpj)
            .IsUnique();

        base.OnModelCreating(modelBuilder);
    }
}