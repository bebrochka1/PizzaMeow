using PizzaMeow.Data.DataProcessing.OrderProccessing;
using PizzaMeow.Data.Models;

namespace PizzaMeow.Data.Repos
{
    public interface IOrderRepository
    {
        IQueryable<Order> GetOrders(OrderFilter? filter, OrderSort? sortBy);
        Task AddOrderAsync(Order order);
        Task DeleteOrderAsync(int Id);
        Task<Order> GetOrderAsync(int Id);
        Task UpdateOrderStatus(int orderId, Status status);
        Task SaveAsync();
    }
}
