using FluentValidation;
using PizzaMeow.Application.DTOs;
using PizzaMeow.Data.Models;
using System.Text.RegularExpressions;

namespace PizzaMeow.Data.Validation
{
    public class UserValidator : AbstractValidator<UserRegisterDTO>
    {
        public UserValidator()
        {
            RuleFor(u => u.Name)
                .NotEmpty()
                .NotNull()
                .MaximumLength(20).WithMessage("Username should be less than 20 characters")
                .MinimumLength(4).WithMessage("Username should be greater than 4 characters");
            RuleFor(u => u.PhoneNumber)
                .NotEmpty()
                .NotNull()
                .MinimumLength(10).WithMessage("Phone number length should be greater than 10 digits")
                .MaximumLength(20).WithMessage("Phone number length should be less than 20 digits");
            RuleFor(u => u.Email)
                .NotEmpty()
                .NotNull()
                .EmailAddress().WithMessage("Email address not valid");
        }
    }
}
