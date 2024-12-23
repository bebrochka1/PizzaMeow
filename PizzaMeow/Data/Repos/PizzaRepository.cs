using Microsoft.EntityFrameworkCore;
using PizzaMeow.Data.DataProcessing;
using PizzaMeow.Data.DataProcessing.PizzaProccessing;
using PizzaMeow.Data.Models;

namespace PizzaMeow.Data.Repos
{
    public class PizzaRepository : IPizzaRepository, IDisposable
    {
        private readonly AppDbContext _context;
        private bool _disposed = false;

        public PizzaRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task AddPizzaAsync(PizzaDTO pizzaDTO)
        {
            var pizza = new Pizza
            {
                Name = pizzaDTO.Name,
                Description = pizzaDTO.Name,
                Price = pizzaDTO.Price
            };

            await _context.Pizzas.AddAsync(pizza);
        }

        public async Task DeletePizzaAsync(int Id)
        {
            var pizzaInDb = await _context.Pizzas.FirstOrDefaultAsync(p => p.Id == Id);
            if (pizzaInDb == null) throw new ArgumentNullException($"Pizza with Id {Id} not found");
            else _context.Pizzas.Remove(pizzaInDb);
        }

        public async Task<Pizza> GetPizzaAsync(int Id)
        {
            var pizzaInDb = await _context.Pizzas.FindAsync(new object[] { Id });
            if (pizzaInDb != null) return pizzaInDb;
            else return null;
        }

        public async Task<List<Pizza>> GetPizzaAsync(string name) => await _context.Pizzas.Where(p => p.Name.Contains(name)).ToListAsync();

        public async Task<PageResults<Pizza>> GetPizzasAsync(PizzaFilter? filter, PizzaSort? sortBy, PizzaPagination? pagination)
        {
            return await _context.Pizzas.Filter(filter).Sort(sortBy).GetPage(pagination);
        }

        public async Task UpdatePizzaAsync(int Id, Pizza pizza)
        {
            var pizzaInDb = await _context.Pizzas.FindAsync(new object[] { Id });
            if(pizzaInDb != null && pizza != null)
            {
                pizzaInDb.Name = pizza.Name;
                pizzaInDb.Description = pizza.Description;
                pizzaInDb.Price = pizza.Price;
            }
        }

        public async Task Save() => await _context.SaveChangesAsync();

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
