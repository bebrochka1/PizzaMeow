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

public record class UserLoginDTO(string Email, string Password);
public record class UserRegisterDTO(string Name, string Email, string PhoneNumber, string Password);