using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PizzaMeow.Application.DTOs
{
    public class PizzaDTO
    {
        public string? Name {  get; set; }
        public string? Description { get; set; }
        public decimal Price { get; set; }
    }
}
