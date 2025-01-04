using Microsoft.EntityFrameworkCore;
using PizzaMeow.Application.DataProcessing.ModelsExtensions;
using PizzaMeow.Data;
using PizzaMeow.Data.DataProcessing.OrderProccessing;
using PizzaMeow.Data.DataProcessing.PizzaProccessing;
using PizzaMeow.Data.Models;
using PizzaMeow.Data.Repos;

namespace PizzaMeow.Infrastructure.DataAccess.Repositories
{
    public class OrderRepository : IOrderRepository, IDisposable
    {
        private bool _disposed = false;
        private readonly AppDbContext _context;

        public OrderRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task AddOrderAsync(Order order) => await _context.Orders.AddAsync(order);

        public async Task DeleteOrderAsync(int Id)
        {
            var orderInDb = await _context.Orders.FindAsync(new object[] { Id });
            if (orderInDb != null) _context.Orders.Remove(orderInDb);
        }

        public async Task<Order> GetOrderAsync(int Id)
        {
            var orderInDb = await _context.Orders.FindAsync(new object[] { Id });
            if (orderInDb != null) return orderInDb;
            else return null;
        }

        public async Task<List<Order>> GetOrderAsync() => await _context.Orders.ToListAsync();

        public IQueryable<Order> GetOrders(OrderFilter? filter, OrderSort? sortBy)
        {
            return  _context.Orders.Filter(filter).Sort(sortBy);
        }

        public async Task UpdateOrderStatus(int orderId, Status status)
        {
            var orderInDb = await _context.Orders.FindAsync(new object[] { orderId });
            if (orderInDb != null) orderInDb.OrderStatus = status;
        }

        public async Task UpdateOrderAsync(int orderId, Order order)
        {
            var orderInDb = await _context.Orders.FindAsync(new object[] { orderId });
            if (orderInDb != null)
            {
                orderInDb.Adress = order.Adress;
                orderInDb.OrderStatus = order.OrderStatus;
                orderInDb.CustomerName = order.CustomerName;
                orderInDb.OrderDetails = order.OrderDetails;
            }
        }

        public async Task SaveAsync() => await _context.SaveChangesAsync();

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _context?.Dispose();
                }

                _disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
