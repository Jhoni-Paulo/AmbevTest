using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ambev.DeveloperEvaluation.ORM.Mapping;

public class SaleItemConfiguration : IEntityTypeConfiguration<SaleItem>
{
    public void Configure(EntityTypeBuilder<SaleItem> builder)
    {
        builder.ToTable("SaleItems");

        builder.HasKey(i => i.Id);
        builder.Property(i => i.Id).HasColumnType("uuid").HasDefaultValueSql("gen_random_uuid()");

        builder.Property(i => i.ProductName).IsRequired().HasMaxLength(200);
        builder.Property(i => i.UnitPrice)
            .HasConversion(money => money.Value, value => (Money)value)
            .HasColumnType("decimal(18,2)");

        builder.Property(i => i.Discount).HasColumnType("decimal(5,2)");
        builder.Property(i => i.TotalPrice)
            .HasConversion(money => money.Value, value => (Money)value)
            .HasColumnType("decimal(18,2)");
    }
}