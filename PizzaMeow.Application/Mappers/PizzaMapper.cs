using PizzaMeow.Application.DTOs;
using PizzaMeow.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PizzaMeow.Application.Mappers
{
    public static class PizzaMapper
    {
        public static Pizza ToEntity(this PizzaDTO pizzaDTO)
        {
            return new Pizza
            {
                Name = pizzaDTO.Name,
                Description = pizzaDTO.Name,
                Price = pizzaDTO.Price
            };
        }

        public static PizzaDTO ToDto(this Pizza pizza)
        {
            return new PizzaDTO
            {
                Name = pizza.Name,
                Description = pizza.Description,
                Price = pizza.Price
            };
        }
    }
}
