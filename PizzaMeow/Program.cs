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
using PizzaMeow.Application.DTOs;
using PizzaMeow.Data;
using PizzaMeow.Data.Models;
using PizzaMeow.Data.Repos;
using PizzaMeow.Data.Validation;
using PizzaMeow.GoogleMaps;
using PizzaMeow.Infrastructure.DataAccess.Repositories;
using PizzaMeow.OrderService;
using PizzaMeow.TelegramBotService;
using PizzaMeow.TelegramBotService.Controllers;
using System.Security.Claims;
using PizzaMeow.Infrastructure;
using PizzaMeow.Application.Services.PizzaSerivice;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

var configuration = builder.Configuration;

builder.Configuration.AddJsonFile("C:\\Users\\User\\source\\repos\\PizzaMeow\\PizzaMeow\\appsettings.Secret.json");

Register(builder.Services);

var app = builder.Build();

Configure(app);

var pizzaApi = app.Services.GetService<PizzaApi>();
var orderApi = app.Services.GetService<OrderApi>();
var userApi = app.Services.GetService<UserApi>();
var authApi = app.Services.GetService<AuthApi>();

pizzaApi?.Register(app);
orderApi?.Register(app);
userApi?.Register(app);
authApi?.Register(app);

app.MapGet("/", () => "Hello World!");

app.Run();

void Register(IServiceCollection services)
{
    services.AddEndpointsApiExplorer();
    services.AddSwaggerGen(options =>
    {
        options.SwaggerDoc("v1", new OpenApiInfo
        {
            Title = "PizzaMeow API",
            Version = "v1",
            Description = "Pizza ordering API"
        });
    });

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
    services.AddTransient<PizzaService>();

    services.AddScoped<IOrderRepository, OrderRepository>();
    services.AddScoped<IPizzaRepository, PizzaRepository>();

    services.AddScoped<IUserRepository, UserRepository>();
    services.AddScoped<IRoleRepository, RoleRepository>();

    services.AddTransient<PizzaApi>();
    services.AddTransient<OrderApi>();
    services.AddTransient<UserApi>();
    services.AddTransient<AuthApi>();
    //services.AddTransient<IApi, TelegramBotApi>();

    services.AddScoped<IValidator<UserRegisterDTO>, UserValidator>();
    services.AddScoped<IValidator<PizzaDTO>, PizzaValidator>();

    services.AddHttpClient<TelegramBotService>().ConfigureHttpClient(
        client =>
        {
            client.BaseAddress = new Uri("https://api.telegram.org.bot");
        }
        );

    services.AddTransient<GoogleMapsService>();
    services.AddSingleton(new TelegramBotService(configuration.GetSection("TelegramBotApi")["ApiKey"]!.ToString(), new HttpClient()));
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
        app.UseSwaggerUI(c =>
        {
            c.SwaggerEndpoint("/swagger/v1/swagger.json", "PizzaMeow API v1");
            c.RoutePrefix = string.Empty;
        });
    }

    app.UseHttpsRedirection();
}
