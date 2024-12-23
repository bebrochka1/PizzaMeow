using Microsoft.EntityFrameworkCore;
using PizzaMeow.Data.DataProcessing;
using PizzaMeow.Data.Models;
using System.Linq.Expressions;

namespace PizzaMeow.Data.DataProcessing.PizzaProccessing
{
    public static class PizzarExt
    {
        public static IQueryable<Pizza> Filter(this IQueryable<Pizza> pizzas, PizzaFilter filter)
        {
            if (!string.IsNullOrEmpty(filter.Name))
            {
                pizzas = pizzas.Where(p => p.Name == filter.Name);
            }

            if (filter.Price > 0)
            {
                pizzas = pizzas.Where(p => p.Price == filter.Price);
            }

            return pizzas;
        }

        public static async Task<PageResults<Pizza>> GetPage(this IQueryable<Pizza> pizzas, PizzaPagination pagination)
        {
            int count = await pizzas.CountAsync();
            if (count == 0)
            {
                return new PageResults<Pizza>(new List<Pizza>(), 0);
            }

            var page = pagination.Page ?? 1;
            var take = pagination.Amount ?? 10;
            var skip = (page - 1) * take;

            return new PageResults<Pizza>(await pizzas.Skip(skip).Take(take).ToListAsync(), count);
        }

        public static IQueryable<Pizza> Sort(this IQueryable<Pizza> pizzas, PizzaSort sort)
        {
            return sort.Direction == OrderDirection.Descending ? pizzas.OrderByDescending(GetKeySelector(sort.OrderBy)) : pizzas.OrderBy(GetKeySelector(sort.OrderBy));
        }

        private static Expression<Func<Pizza, object>> GetKeySelector(string property)
        {
            if (string.IsNullOrEmpty(property))
            {
                return p => p.Name;
            }
            else
            {
                return property switch
                {
                    nameof(Pizza.Price) => p => p.Price,
                    nameof(Pizza.Description) => p => p.Description,
                    _ => p => p.Name
                };
            }
        }
    }
}
