using ERPFoundation.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ERPFoundation.Infrastructure.Data.Configurations;

public class ProductConfiguration : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.HasKey(p => p.Id);

        builder.Property(p => p.Name)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(p => p.Sku)
            .IsRequired()
            .HasMaxLength(50);

        builder.Property(p => p.Price)
            .HasColumnType("decimal(10,2)");

        builder.HasOne(p => p.Supplier)
            .WithMany(f => f.Products)
            .HasForeignKey(p => p.SupplierId);
    }
}