using PizzaMeow.Application.DTOs;
using PizzaMeow.Application.Mappers;
using PizzaMeow.Data.DataProcessing;
using PizzaMeow.Data.DataProcessing.PizzaProccessing;
using PizzaMeow.Data.Models;
using PizzaMeow.Data.Repos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PizzaMeow.Application.Services.PizzaSerivice
{
    public class PizzaService
    {
        private readonly IPizzaRepository _queryRepository;

        public PizzaService(IPizzaRepository queryRepository)
        {
            _queryRepository = queryRepository;
        }

        public async Task<PageResults<PizzaDTO>> GetPageResults(PizzaFilter filter, PizzaSort sortBy, PizzaPagination pagination)
        {
            var query = _queryRepository.GetPizzas(filter, sortBy);
            var pageResults = await query.GetPage(pagination);

            var dtos = pageResults.Values.Select(p => p.ToDto());

            return new PageResults<PizzaDTO>(dtos.ToList(), dtos.Count());
        }
    }
}
