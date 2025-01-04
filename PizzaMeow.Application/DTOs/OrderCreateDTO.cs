using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PizzaMeow.Application.DTOs
{
    public class OrderCreateDTO
    {
        public string? CustomerName { get; set; }
        public string? Adress { get; set; }
        public ICollection<OrderDetailsCreateDTO> OrderDetails { get; set; } = [];
        public string? PaymentMethod { get; set; }
    }
}
