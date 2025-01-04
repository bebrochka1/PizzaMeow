using FluentValidation;
using PizzaMeow.Application.DTOs;
using PizzaMeow.Data.Models;

namespace PizzaMeow.Data.Validation
{
    public class PizzaValidator : AbstractValidator<PizzaDTO>
    {
        public PizzaValidator()
        {
            RuleFor(p => p.Name)
                .NotEmpty()
                .NotNull();
            RuleFor(p => p.Description)
                .MaximumLength(400);
            RuleFor(p => p.Price).Must(p => p >= 0);
        }
    }
}
