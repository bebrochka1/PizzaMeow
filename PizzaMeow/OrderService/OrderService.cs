using Microsoft.IdentityModel.Tokens;
using PizzaMeow.Data;
using PizzaMeow.Data.Models;
using PizzaMeow.Data.Repos;

namespace PizzaMeow.OrderService;

public class OrderService
{
    private readonly AppDbContext _context;
    private readonly IPizzaRepository _repository;

    public OrderService(AppDbContext context, IPizzaRepository repository)
    {
        _context = context;
        _repository = repository;
    }

    public async Task<Order> CreateOrderAsync(OrderCreateDTO orderDTO)
    {
        if (orderDTO == null || orderDTO.OrderDetails == null || orderDTO.OrderDetails.Count == 0)
        {
            return null;
        }

        bool isPaymentMethodParsed = Enum.TryParse(orderDTO.PaymentMethod, out PaymentMethod paymentMethod);

        if (!isPaymentMethodParsed) 
        {
            throw new ArgumentException("Payment method has incorrect format");
        }

        var Order = new Order()
        {
            CustomerName = orderDTO.CustomerName,
            Adress = orderDTO.Adress,
            OrderDate = DateTime.UtcNow,
            OrderStatus = Status.Pending,
            OrderDetails = new List<OrderDetails>(),
            PaymentMethod = paymentMethod
        };

        foreach (var orderDetailDTO in orderDTO.OrderDetails)
        {
            var pizza = await _repository.GetPizzaAsync(orderDetailDTO.PizzaId);
            if(pizza == null)
            {
                throw new ArgumentException("Wrong pizza Id");
            }

            var orderDetail = new OrderDetails()
            {
                PizzaId = pizza.Id,
                OrderId = Order.Id,
                Quantity = orderDetailDTO.Quantity,
                TotalPrice = pizza.Price * orderDetailDTO.Quantity,
            };

            Order.TotalAmount += orderDetail.TotalPrice;

            Order.OrderDetails.Add(orderDetail);
        }

        return Order;
    }
}
