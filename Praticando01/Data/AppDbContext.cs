using Microsoft.EntityFrameworkCore;
using Praticando01.Models;

namespace Praticando01.Data;

public class AppDbContext : DbContext
{
    public DbSet<Produto> Produtos { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        // Ajuste a senha localmente antes de executar
        string connectionString = "Server=localhost;Database=erpfoundation;User=root;Password=SUA_SENHA;";

        optionsBuilder.UseMySql(
            connectionString,
            ServerVersion.AutoDetect(connectionString));
    }
}