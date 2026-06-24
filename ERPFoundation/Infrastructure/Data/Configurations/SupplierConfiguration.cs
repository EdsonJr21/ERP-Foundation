using ERPFoundation.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ERPFoundation.Infrastructure.Data.Configurations;

public class SupplierConfiguration : IEntityTypeConfiguration<Supplier>
{
    public void Configure(EntityTypeBuilder<Supplier> builder)
    {
        builder.HasKey(f => f.Id);

        builder.Property(f => f.Name)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(f => f.TaxId)
            .IsRequired()
            .HasMaxLength(18);

        builder.HasIndex(f => f.TaxId)
            .IsUnique();
    }
}