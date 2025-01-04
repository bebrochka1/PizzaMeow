using System.Text.Json.Serialization;

namespace PizzaMeow.Data.Models;

public class OrderDetails
{
    public int Id { get; set; }
    public int OrderId { get; set; }
    public int PizzaId { get; set; }
    public int Quantity { get; set; }
    public decimal TotalPrice { get; set; }
    [JsonIgnore]
    public Order? Order { get; set; }
    [JsonIgnore]
    public Pizza? Pizza {  get; set; }
}
