using Microsoft.EntityFrameworkCore;
using PizzaMeow.Data.Models;
using System.Reflection;

namespace PizzaMeow.Data;

public class AppDbContext : DbContext 
{
    private readonly IConfiguration _configuration; 
    public AppDbContext(DbContextOptions<AppDbContext> options, IConfiguration configuration) : base(options)
    {
        _configuration = configuration;
    }

    public DbSet<User> Users => Set<User>();
    public DbSet<Pizza> Pizzas => Set<Pizza>();
    public DbSet<Order> Orders => Set<Order>();
    public DbSet<OrderDetails> OrderDetails => Set<OrderDetails>();
    public DbSet<Role> Roles => Set<Role>();

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>().HasData(
            new User
            {
                Id = 1,
                Name = _configuration.GetSection("Admin")["Name"],
                Email = _configuration.GetSection("Admin")["Email"],
                PhoneNumber = _configuration.GetSection("Admin")["Phone"],
                RoleId = 1,
                PasswordHashed = BCrypt.Net.BCrypt.EnhancedHashPassword(_configuration.GetSection("Admin")["Password"], BCrypt.Net.HashType.SHA256)
            },
            new User
            {
                Id = 2,
                Name = _configuration.GetSection("User")["Name"],
                Email = _configuration.GetSection("User")["Email"],
                PhoneNumber = _configuration.GetSection("User")["Phone"],
                RoleId = 2,
                PasswordHashed = BCrypt.Net.BCrypt.EnhancedHashPassword(_configuration.GetSection("User")["Password"], BCrypt.Net.HashType.SHA256)
            },
            new User
            {
                Id = 3,
                Name = _configuration.GetSection("Courier")["Name"],
                Email = _configuration.GetSection("Courier")["Email"],
                PhoneNumber = _configuration.GetSection("Courier")["Phone"],
                RoleId = 3,
                PasswordHashed = BCrypt.Net.BCrypt.EnhancedHashPassword(_configuration.GetSection("Courier")["Password"], BCrypt.Net.HashType.SHA256)
            }); ;

        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}
