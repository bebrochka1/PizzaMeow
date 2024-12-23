using System.Text.Json.Serialization;

namespace PizzaMeow.Data.Models;
//TODO Model validation
public class Pizza
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public ICollection<OrderDetails> OrderDetails { get; set; } = new List<OrderDetails>();
}

public record class PizzaDTO(string Name, string Description, decimal Price);
