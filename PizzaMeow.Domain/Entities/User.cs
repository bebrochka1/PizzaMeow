namespace PizzaMeow.Data.Models;
//TODO Model validation
public class User
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string PhoneNumber { get; set; } = string.Empty;
    public string PasswordHashed { get; set; } = string.Empty;
    public int RoleId { get; set; }
    public Role? UserRole { get; set; }
}


