using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PizzaMeow.Data.Models;
using PizzaMeow.Data.Repos;
using PizzaMeow.GoogleMaps;
using PizzaMeow.OrderService;
using PizzaMeow.TelegramBotService.Controllers;
using System.Text;

namespace PizzaMeow.Apis
{
    public class OrderApi : IApi
    {
        public void Register(WebApplication app) 
        {
            app.MapPost("/orders", async (OrderService.OrderService orderService, [FromBody] OrderCreateDTO orderDTO, IOrderRepository orderRepository) =>
            {
                var order = await orderService.CreateOrderAsync(orderDTO);
                await orderRepository.AddOrderAsync(order);
                await orderRepository.SaveAsync();
                return Results.Created();
            })
                .Accepts<OrderCreateDTO>("application/json")
                .Produces(StatusCodes.Status201Created)
                .WithName("Post new order")
                .WithTags("Order");

            app.MapGet("/orders/{Id}", [Authorize(Roles = "Admin, Courier")] async (IOrderRepository repository, int Id, HttpContext context) =>
            {
                var orderInDb = await repository.GetOrderAsync(Id);
                if (orderInDb != null)
                {
                    return Results.Ok(orderInDb);
                }
                else
                {
                    return Results.NotFound();
                }
            })
                .Produces<Order>(StatusCodes.Status200OK)
                .WithName("Get order by ID")
                .WithTags("Order");

            app.MapDelete("/orders/{Id}", [Authorize(Roles = "Admin")] async (IOrderRepository repos, int orderId) =>
            {
                await repos.DeleteOrderAsync(orderId);
                await repos.SaveAsync();
            })
                .Produces(StatusCodes.Status200OK)
                .WithName("Delete order by ID")
                .WithTags("Order");

            app.MapGet("/orders", [Authorize(Roles = "Admin, Courier")] async (IOrderRepository orderRepository) => await orderRepository.GetOrderAsync())
                .Produces<List<Order>>(StatusCodes.Status200OK)
                .WithName("Get all orders")
                .WithTags("Order");

            app.MapPut("/order/{orderId}/status/{statusId}", [Authorize(Roles = "Admin, Courier")] async (int orderId, Status status, IOrderRepository repos) =>
            {
                if (orderId <= 0 || status <= 0)
                {
                    return Results.BadRequest("Invalid Id provided");
                }
                var orderInDb = await repos.GetOrderAsync(orderId);
                await repos.UpdateOrderStatus(orderId, status);
                await repos.SaveAsync();
                return Results.Ok();
            })
                .Produces(StatusCodes.Status200OK)
                .WithName("Change order status by ID manually")
                .WithTags("Order");

            app.MapPatch("/order/{Id}/setouttodelivery", async (int orderId, IOrderRepository orderRepository) => 
            {
                var orderInDb = await orderRepository.GetOrderAsync(orderId);
                
                if(orderInDb is null)
                {
                    return Results.BadRequest("Order with current id does not exist");
                }

                if(orderInDb.OrderStatus == Status.OutToDelivery)
                {
                    return Results.Ok();
                }

                orderInDb.OrderStatus = Status.OutToDelivery;
                await orderRepository.SaveAsync();
                return Results.Ok();
            })
                .Produces(StatusCodes.Status200OK)
                .WithName("Gets order to work for couriers by Id")
                .WithTags("Order");

            app.MapPatch("/order/{Id}/cancelorder", async (int orderId, IOrderRepository orderRepository) =>
            {
                var orderInDb = await orderRepository.GetOrderAsync(orderId);

                if (orderInDb is null)
                {
                    return Results.BadRequest("Order with current id does not exist");
                }

                if (orderInDb.OrderStatus == Status.Cancelled)
                {
                    return Results.Ok();
                }

                orderInDb.OrderStatus = Status.Cancelled;
                await orderRepository.SaveAsync();
                return Results.Ok();
            })
                .Produces(StatusCodes.Status200OK)
                .WithName("Cancels the order by Id")
                .WithTags("Order");

            app.MapPatch("/order/{Id}/setorderfinished", async (int orderId, IOrderRepository orderRepository) =>
            {
                var orderInDb = await orderRepository.GetOrderAsync(orderId);

                if (orderInDb is null)
                {
                    return Results.BadRequest("Order with current id does not exist");
                }

                if (orderInDb.OrderStatus == Status.Cancelled)
                {
                    return Results.Ok();
                }

                orderInDb.OrderStatus = Status.Cancelled;
                await orderRepository.SaveAsync();
                return Results.Ok();
            })
                .Produces(StatusCodes.Status200OK)
                .WithName("Marks order finished")
                .WithTags("Order");

            app.MapPost("/orders/getToWork", async (
                OrderService.OrderService orderService,
                [FromBody] OrderCreateDTO orderDTO,
                IOrderRepository orderRepository,
                IPizzaRepository pizzaRepository,
                TelegramBotService.TelegramBotService telegramBot,
                HttpClient httpClient,
                GoogleMapsService googleMaps) =>
            {
                if (orderDTO is null) return Results.BadRequest("Data was not provided");
                
                Order order;
                
                try
                {
                    order = await orderService.CreateOrderAsync(orderDTO);
                } 
                catch(ArgumentException ex)
                {
                    return Results.BadRequest($"{ex.Message}");
                }

                var pizzasIds = order.OrderDetails.Select(od => od.PizzaId);

                var pizzas = new List<Pizza>();

                foreach(int id in pizzasIds)
                {
                    var pizza = await pizzaRepository.GetPizzaAsync(id);
                    if (pizza is null || string.IsNullOrEmpty(pizza.Name) || string.IsNullOrEmpty(pizza.Description))
                        return Results.BadRequest("Pizza in order details is null or empty");
                    pizzas.Add(pizza);
                }

                (double, double) coordinates = await googleMaps.GetCoordinatesAsync(orderDTO.Adress);

                var sb = new StringBuilder();

                sb.Append("😃 New Order! 🍕\n");
                sb.Append($"Customer name: {orderDTO.CustomerName}\n");
                sb.Append($"🏠 Adress: {orderDTO.Adress}\n");

                sb.Append("Order Details:\n");

                foreach (var pizza in pizzas)
                {
                    sb.AppendLine(order.OrderDetails.FirstOrDefault(p => p.PizzaId == pizza.Id).Quantity + " - " + pizza.Name);
                }

                sb.AppendLine("💲 Total amount: $" + order.TotalAmount);
                switch (order.PaymentMethod) {
                    case PaymentMethod.CreditCard:
                        sb.AppendLine($"Payment method: Credit Card 💳");
                        break;
                    case PaymentMethod.Cash:
                        sb.AppendLine($"Payment method: Cash 💵");
                        break;
                }

                sb.AppendLine("Status: ⬜️ Pending");

                var message = new MessageRequestDTO("-4681601102", sb.ToString());
                await orderRepository.AddOrderAsync(order);
                await orderRepository.SaveAsync();

                await telegramBot.SendMessageAsync(message.ChatId, message.Message);
                await telegramBot.SendCoordinatesAsync(message.ChatId, coordinates);    

                return Results.Ok();
            });
        }
    }
}
