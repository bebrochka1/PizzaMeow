using PizzaMeow.Data.DataProcessing;
using PizzaMeow.Data.DataProcessing.PizzaProccessing;
using PizzaMeow.Data.Models;

namespace PizzaMeow.Data.Repos
{
    public interface IPizzaRepository
    {
        IQueryable<Pizza> GetPizzas(PizzaFilter? filter, PizzaSort? sortBy);
        Task<Pizza> GetPizzaAsync(int Id);
        Task<List<Pizza>> GetPizzaAsync(string name);
        Task AddPizzaAsync(Pizza pizza);
        Task DeletePizzaAsync(int Id);
        Task UpdatePizzaAsync(int Id, Pizza pizza);
        Task Save();
    }
}
