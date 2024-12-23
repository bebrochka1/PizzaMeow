using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PizzaMeow.Data.DataProcessing.PizzaProccessing;
using PizzaMeow.Data.Models;
using PizzaMeow.Data.Repos;

namespace PizzaMeow.Apis
{
    public class PizzaApi : IApi
    {
        public void Register(WebApplication app) 
        {
            app.MapGet("/pizza", async (IPizzaRepository repository, [AsParameters] PizzaFilter filter, [AsParameters] PizzaSort sortBy, [AsParameters] PizzaPagination pagination)
                => await repository.GetPizzasAsync(filter, sortBy, pagination))
                .Produces<List<Pizza>>(StatusCodes.Status200OK)
                .WithName("Get all pizza")
                .WithTags("Pizza");

            app.MapGet("/pizza/{Id}", async (IPizzaRepository repository, int Id) => await repository.GetPizzaAsync(Id))
                .Produces<Pizza>(StatusCodes.Status200OK)
                .WithName("Get pizza by ID")
                .WithTags("Pizza");

            app.MapGet("/pizza/name/{name}", async (string name, IPizzaRepository repos)
                => await repos.GetPizzaAsync(name))
                .Produces<List<Pizza>>(StatusCodes.Status200OK)
                .Produces(StatusCodes.Status404NotFound)
                .WithName("Get pizza by name")
                .WithTags("Pizza")
                .ExcludeFromDescription();

            app.MapPost("/pizza", [Authorize(Roles = "Admin")] async (IPizzaRepository repository, [FromBody] PizzaDTO pizzaDTO, IValidator<PizzaDTO> validator) =>
            {
                if (pizzaDTO == null) return Results.BadRequest("Data is not provided");
                
                else
                {
                    var validationResults = await validator.ValidateAsync(pizzaDTO);
                    if(validationResults.IsValid)
                    {
                        await repository.AddPizzaAsync(pizzaDTO);
                        await repository.Save();
                        return Results.Created();
                    }
                    else
                    {
                        return Results.ValidationProblem(validationResults.ToDictionary());
                    }
                }
            })
                .Accepts<Pizza>("application/json")
                .Produces(StatusCodes.Status201Created)
                .WithName("Post new pizza")
                .WithTags("Pizza");

            app.MapPut("/pizza/{Id}", [Authorize(Roles = "Admin")] async (int Id, IPizzaRepository repository, [FromBody] Pizza pizza) =>
            {
                var pizzaInDb = await repository.GetPizzaAsync(Id);
                if (pizzaInDb == null) return Results.NotFound();
                if (string.IsNullOrEmpty(pizza.Name) || string.IsNullOrEmpty(pizza.Description) || pizza.Price <= 0) return Results.BadRequest("Data was not provided");
                else
                {
                    await repository.UpdatePizzaAsync(Id, pizza);
                    await repository.Save();
                    return Results.NoContent();
                }
            })
                .Accepts<Pizza>("application/json")
                .WithName("Update pizza")
                .WithTags("Pizza");

            app.MapDelete("/pizza/{Id}", [Authorize(Roles = "Admin")] async (IPizzaRepository repository, int Id) =>
            {
                var pizzaInDb = await repository.GetPizzaAsync(Id);
                if (pizzaInDb == null) return Results.NotFound();
                else
                {
                    await repository.DeletePizzaAsync(Id);
                    await repository.Save();
                    return Results.Ok();
                }
            })
                .Produces(StatusCodes.Status200OK)
                .WithName("Delete pizza by ID")
                .WithTags("Pizza");
        }
    }
}
