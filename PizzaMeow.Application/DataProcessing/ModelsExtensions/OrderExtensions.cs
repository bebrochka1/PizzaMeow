using Microsoft.EntityFrameworkCore;
using PizzaMeow.Data.DataProcessing;
using PizzaMeow.Data.DataProcessing.OrderProccessing;
using PizzaMeow.Data.Models;
using System.Linq.Expressions;

namespace PizzaMeow.Application.DataProcessing.ModelsExtensions;

public static class OrderExtensions
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

        if (filter?.PaymentMethod != null)
        {
            orders = filter.PaymentMethod == PaymentMethod.CreditCard
                ? orders.Where(o => o.PaymentMethod == PaymentMethod.CreditCard)
                : orders.Where(o => o.PaymentMethod == PaymentMethod.Cash);
        }

        if (filter?.TotalPrice != null)
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
        return sort.OrderSortDirection == OrderSortDirection.Ascending ? orders.OrderBy(GetKeySelector(sort.Property)) : orders.OrderByDescending(GetKeySelector(sort.Property));
    }

    public static Expression<Func<Order, object>> GetKeySelector(string property)
    {
        if (property == null)
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

    public static async Task<PageResults<Order>> GetPage(this IQueryable<Order> orders, OrderPagination pagination)
    {
        int count = await orders.CountAsync();
        if (count == 0)
        {
            return new PageResults<Order>(new List<Order>(), 0);
        }

        int page = pagination.Page ?? 1;
        int take = pagination.Amount ?? 10;
        int skip = (page - 1) * take;

        return new PageResults<Order>(await orders.Skip(skip).Take(take).ToListAsync(), orders.Count());
    }
}
