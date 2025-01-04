using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PizzaMeow.Application.DTOs
{
    public class OrderDetailsCreateDTO
    {
        public int Quantity { get; set; }
        public int PizzaId { get; set; }
    }
}
