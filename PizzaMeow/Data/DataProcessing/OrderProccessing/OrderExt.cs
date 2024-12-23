using PizzaMeow.Data.Models;
using System.Linq.Expressions;

namespace PizzaMeow.Data.DataProcessing.OrderProccessing
{
    public static class OrderExt
    {
        public static IQueryable<Order> Filter(this IQueryable<Order> orders, OrderFilter? filter)
        {
            if (filter?.DateTime != null)
            {
                if (filter.Direction != null)
                {
                    orders = filter.Direction == DateDirection.After 
                        ? orders.Where(o => o.OrderDate >= filter.DateTime)
                        : orders.Where(o => o.OrderDate <= filter.DateTime);
                }
                else
                {
                    orders = orders.Where(o => o.OrderDate > filter.DateTime);
                }
            }

            if(filter?.PaymentMethod != null)
            {
                orders = filter.PaymentMethod == PaymentMethod.CreditCard
                    ? orders.Where(o => o.PaymentMethod == PaymentMethod.CreditCard)
                    : orders.Where(o => o.PaymentMethod == PaymentMethod.Cash);
            }

            if(filter?.TotalPrice != null)
            {
                orders = filter.LessOrTMore == LessOrMore.Less
                    ? orders.Where(o => o.TotalAmount < filter.TotalPrice)
                    : orders.Where(o => o.TotalAmount > filter.TotalPrice);
            }

            if (filter?.Status != null)
            {
                orders = orders.Where(o => o.OrderStatus == filter.Status);
            }

            return orders;
        }

        public static IQueryable<Order> Sort(this IQueryable<Order> orders, OrderSort? sort) 
        {
            return sort.Direction == OrderDirection.Ascending ? orders.OrderBy(GetKeySelector(sort.Property)) : orders.OrderByDescending(GetKeySelector(sort.Property));
        }

        public static Expression<Func<Order, object>> GetKeySelector(string property)
        {
            if(property == null)
            {
                return p => p.OrderDate;
            }
            else
            {
                return property switch
                {
                    nameof(Order.TotalAmount) => o => o.TotalAmount,
                    nameof(Order.CustomerName) => o => o.CustomerName,
                    _ => o => o.OrderDate
                };
            }
        }

        public static PageResults<Order> GetPage(this IQueryable<Order> orders, OrderPagination pagination)
        {
            if (orders.Count() == 0) 
            {
                return new PageResults<Order>(new List<Order>(), 0);
            }

            int page = pagination.Page ?? 1;
            int amount = pagination.Amount ?? 10;

            int skip = (page - 1) * amount;

            var results = orders.Skip(skip).Take(amount);

            return new PageResults<Order>(results.ToList(), orders.Count());
        }
    }
}
