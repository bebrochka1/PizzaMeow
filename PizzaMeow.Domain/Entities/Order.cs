using System.Text.Json.Serialization;

namespace PizzaMeow.Data.Models;
public class Order
{
    public int Id { get; set; }
    public string CustomerName { get; set; } = string.Empty;
    public string Adress {  get; set; } = string.Empty;
    public DateTime OrderDate { get; set; }
    public decimal TotalAmount { get; set; }
    [JsonIgnore]
    public ICollection<OrderDetails> OrderDetails { get; set; } = [];
    [JsonIgnore]
    public Status? OrderStatus { get; set; }
    public PaymentMethod? PaymentMethod { get; set; }
}

public enum PaymentMethod
{
    Cash,
    CreditCard
}

public enum Status
{
    Pending,
    OutToDelivery,
    Finished,
    Cancelled
}
