using BCrypt.Net;
using FluentValidation;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Options;
using PizzaMeow.Apis;
using PizzaMeow.Data;
using PizzaMeow.Data.Models;
using PizzaMeow.Data.Repos;
using PizzaMeow.Data.Validation;
using PizzaMeow.GoogleMaps;
using PizzaMeow.OrderService;
using PizzaMeow.TelegramBotService;
using PizzaMeow.TelegramBotService.Controllers;
using System.Security.Claims;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration.AddJsonFile("C:\\Users\\User\\source\\repos\\PizzaMeow\\PizzaMeow\\appsettings.Secret..json");

Register(builder.Services);
builder.WebHost.UseUrls("http://localhost:5289");


var app = builder.Build();

Configure(app);

var apis = app.Services.GetServices<IApi>();

foreach (var api in apis)
{
    if (api is null) throw new InvalidProgramException("Api not found");
    api.Register(app);
}

app.MapGet("/", () => "Hello World!");

app.Run();

void Register(IServiceCollection services)
{
    services.AddEndpointsApiExplorer();
    services.AddSwaggerGen();

    services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
        .AddCookie(options =>
        {
            options.AccessDeniedPath = "/unauthorized";

            options.Events = new CookieAuthenticationEvents
            {
                OnRedirectToLogin = context =>
                {
                    context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                    return Task.CompletedTask;
                },

                OnRedirectToAccessDenied = context =>
                {
                    context.Response.StatusCode = StatusCodes.Status403Forbidden;
                    return Task.CompletedTask;
                }
            };
        });
    services.AddAuthorization();

    services.AddDbContext<AppDbContext>(options =>
    {
        options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
        options.ConfigureWarnings(warnings =>
        warnings.Ignore(RelationalEventId.PendingModelChangesWarning));

    });

    services.AddTransient<OrderService>();

    services.AddScoped<IPizzaRepository, PizzaRepository>();
    services.AddScoped<IOrderRepository, OrderRepository>();
    services.AddScoped<IUserRepository, UserRepository>();
    services.AddScoped<IRoleRepository, RoleRepository>();

    services.AddTransient<IApi, PizzaApi>();
    services.AddTransient<IApi, OrderApi>();
    services.AddTransient<IApi, UserApi>();
    services.AddTransient<IApi, AuthApi>();
    services.AddTransient<IApi, TelegramBotApi>();

    services.AddScoped<IValidator<UserRegisterDTO>, UserValidator>();
    services.AddScoped<IValidator<PizzaDTO>, PizzaValidator>();

    services.AddHttpClient<TelegramBotService>().ConfigureHttpClient(
        client =>
        {
            client.BaseAddress = new Uri("https://api.telegram.org.bot");
        }
        );

    services.AddTransient<GoogleMapsService>();
    services.AddSingleton(new TelegramBotService("7871029034:AAHkFzCmENIgreKd5UUL7MrowKAdutmZzt8", new HttpClient()));
}

void Configure(WebApplication app) 
{
    app.UseRouting();

    app.UseAuthentication();
    app.UseAuthorization();

    if (app.Environment.IsDevelopment())
    {
        var scope = app.Services.CreateScope();
        var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
        db.Database.Migrate();
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    app.UseHttpsRedirection();
}
