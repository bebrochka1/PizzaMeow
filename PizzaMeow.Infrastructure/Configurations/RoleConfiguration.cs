using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PizzaMeow.Data.Models;

namespace PizzaMeow.Data.Configurations;

public class RoleConfiguration : IEntityTypeConfiguration<Role>
{
    public void Configure(EntityTypeBuilder<Role> builder)
    {
        builder.HasKey(r => r.Id);
        builder.HasData(
            new Role()
            {
                Id = 1,
                RoleName = "Admin",
            },
            new Role()
            {
                Id = 2,
                RoleName = "User"
            },
            new Role()
            {
                Id = 3,
                RoleName = "Courier"
            }
            );

        builder.ToTable("Roles");
    }
}
