using BCrypt.Net;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PizzaMeow.Data.Models;

namespace PizzaMeow.Data.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasKey(u => u.Id);
        builder.HasOne(u => u.UserRole)
            .WithMany(r => r.Users)
            .HasForeignKey(u => u.RoleId);

        builder.ToTable("Users");
    }
}
