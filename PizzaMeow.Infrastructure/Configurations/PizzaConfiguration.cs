using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PizzaMeow.Data.Models;

namespace PizzaMeow.Data.Configurations;

public class PizzaConfiguration : IEntityTypeConfiguration<Pizza>
{
    public void Configure(EntityTypeBuilder<Pizza> builder)
    {
        builder.HasKey(p => p.Id);
        builder.Property(p => p.Price).HasPrecision(18, 2);

        builder.HasData(
            new Pizza
            {
                Id = 1,
                Name = "Margaritta",
                Description = "Margaritta pizza with no meat",
                Price = 9.99M
            },
            new Pizza
            {
                Id = 2,
                Name = "Pepperoni",
                Description = "Classica",
                Price = 14.99M
            },
            new Pizza
            {
                Id = 3,
                Name = "Havaii",
                Description = "Classic pizza with pineapple",
                Price = 11.99M
            }
        );

        builder.ToTable("Pizzas");
    }
}
