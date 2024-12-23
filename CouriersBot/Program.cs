using CouriersBot.TelegramBotService;
using Telegram.Bot;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton<TelegramBotService>();

var app = builder.Build();

app.MapGet("/", () => "Hello World!");

app.Run();