using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PizzaMeow.Data.Models;

namespace PizzaMeow.Data.Configurations;

public class OrderConfiguration : IEntityTypeConfiguration<Order>
{
    public void Configure(EntityTypeBuilder<Order> builder)
    {
        builder.HasKey(o => o.Id);

        builder.Property(o => o.TotalAmount).HasPrecision(18,2);

        builder.Property(o => o.PaymentMethod)
            .HasConversion(
            p => p.ToString(),
            p => (PaymentMethod)Enum.Parse(typeof(PaymentMethod), p));

        builder.Property(o => o.OrderStatus)
            .HasConversion(
            s => s.ToString(),
            s => (Status)Enum.Parse(typeof(Status), s));

        builder.ToTable("Orders");
    }
}
