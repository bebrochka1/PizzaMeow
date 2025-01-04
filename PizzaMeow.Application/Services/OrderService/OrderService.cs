using PizzaMeow.Application.DTOs;
using PizzaMeow.Data;
using PizzaMeow.Data.DataProcessing.PizzaProccessing;
using PizzaMeow.Data.DataProcessing;
using PizzaMeow.Data.Models;
using PizzaMeow.Data.Repos;
using PizzaMeow.Data.DataProcessing.OrderProccessing;
using PizzaMeow.Application.DataProcessing.ModelsExtensions;

namespace PizzaMeow.OrderService;

public class OrderService
{
    private readonly IPizzaRepository _repository;
    private readonly IOrderRepository _queryRepository;

    public OrderService(IPizzaRepository repository, IOrderRepository queryRepository)
    {
        _repository = repository;
        _queryRepository = queryRepository;
    }

    public async Task<PageResults<Order>> GetPageResults(OrderFilter filter, OrderSort sortBy, OrderPagination pagination)
    {
        var query = _queryRepository.GetOrders(filter, sortBy);
        var pageResults = await query.GetPage(pagination);

        return new PageResults<Order>(pageResults.Values, pageResults.Values.Count());
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
