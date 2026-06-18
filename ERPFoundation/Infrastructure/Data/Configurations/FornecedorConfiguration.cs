using ERPFoundation.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ERPFoundation.Infrastructure.Data.Configurations;

public class FornecedorConfiguration : IEntityTypeConfiguration<Fornecedor>
{
    public void Configure(EntityTypeBuilder<Fornecedor> builder)
    {
        builder.HasKey(f => f.Id);

        builder.Property(f => f.Nome)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(f => f.Cnpj)
            .IsRequired()
            .HasMaxLength(18);

        builder.HasIndex(f => f.Cnpj)
            .IsUnique();
    }
}