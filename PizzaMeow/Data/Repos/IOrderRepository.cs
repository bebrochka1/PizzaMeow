using PizzaMeow.Data.Models;

namespace PizzaMeow.Data.Repos
{
    public interface IOrderRepository
    {
        Task AddOrderAsync(Order order);
        Task DeleteOrderAsync(int Id);
        Task<Order> GetOrderAsync(int Id);
        Task<List<Order>> GetOrderAsync();
        Task UpdateOrderStatus(int orderId, Status status);
        Task SaveAsync();
    }
}
