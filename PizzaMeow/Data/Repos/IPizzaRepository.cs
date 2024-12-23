using PizzaMeow.Data.DataProcessing;
using PizzaMeow.Data.DataProcessing.PizzaProccessing;
using PizzaMeow.Data.Models;

namespace PizzaMeow.Data.Repos
{
    public interface IPizzaRepository
    {
        Task<PageResults<Pizza>> GetPizzasAsync(PizzaFilter? filter, PizzaSort? sortBy, PizzaPagination? pagination);
        Task<Pizza> GetPizzaAsync(int Id);
        Task<List<Pizza>> GetPizzaAsync(string name);
        Task AddPizzaAsync(PizzaDTO pizza);
        Task DeletePizzaAsync(int Id);
        Task UpdatePizzaAsync(int Id, Pizza pizza);
        Task Save();
    }
}
