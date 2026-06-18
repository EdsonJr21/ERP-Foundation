using ERPFoundation.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ERPFoundation.Infrastructure.Data.Configurations;

public class ProdutoConfiguration : IEntityTypeConfiguration<Produto>
{
    public void Configure(EntityTypeBuilder<Produto> builder)
    {
        builder.HasKey(p => p.Id);

        builder.Property(p => p.Nome)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(p => p.Sku)
            .IsRequired()
            .HasMaxLength(50);

        builder.Property(p => p.Preco)
            .HasColumnType("decimal(10,2)");

        builder.HasOne(p => p.Fornecedor)
            .WithMany(f => f.Produtos)
            .HasForeignKey(p => p.FornecedorId);
    }
}