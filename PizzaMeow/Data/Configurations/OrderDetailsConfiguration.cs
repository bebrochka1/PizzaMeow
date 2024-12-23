using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PizzaMeow.Data.Models;

namespace PizzaMeow.Data.Configurations;

public class OrderDetailsConfiguration : IEntityTypeConfiguration<OrderDetails>
{
    public void Configure(EntityTypeBuilder<OrderDetails> builder)
    {
        builder.HasKey(od => od.Id);

        builder.Property(od => od.TotalPrice).HasPrecision(18,2);

        builder.HasOne(od => od.Pizza)
            .WithMany(p => p.OrderDetails)
            .HasForeignKey(od => od.PizzaId);

        builder.HasOne(od => od.Order)
            .WithMany(o => o.OrderDetails)
            .HasForeignKey(od => od.OrderId);

        builder.ToTable("OrderDetails");
    }
}
