using FluentValidation;
using PizzaMeow.Application.DTOs;
using PizzaMeow.Data.Models;
using System.Text.RegularExpressions;

namespace PizzaMeow.Data.Validation
{
    public class OrderDTOValidator : AbstractValidator<OrderCreateDTO>
    {
        public OrderDTOValidator() 
        {
            RuleFor(o => o.CustomerName)
                .NotEmpty()
                .NotNull()
                .MaximumLength(100);
            RuleFor(o => o.Adress)
                .NotEmpty()
                .NotNull()
                .Matches(@"^[а-яА-ЯёЁa-zA-Z\s\.\,\-]+,\s*\d+(\s*[а-яА-Яa-zA-Z\d\-\/]*)?$")
                .WithMessage("Adress has incorrect format");
        }
    }
}
