using Microsoft.AspNetCore.Mvc;
using PizzaMeow.Apis;
using Telegram.Bot.Types;

namespace PizzaMeow.TelegramBotService.Controllers
{
    public class TelegramBotApi : IApi
    {
        void IApi.Register(WebApplication app)
        {
            app.MapGet("/telegrambot/setWebhook", async (TelegramBotService telegramBot) =>
            {
                string webhookUrl = "https://3dd2-37-46-252-5.ngrok-free.app/api/Telegram";
                await telegramBot.SetWebHookAsync(webhookUrl);
                return Results.Ok("Webhook set successfully");
            });

            app.MapGet("telegrambot/removewebhook", async (TelegramBotService telegramBot) =>
            {
                await telegramBot.DeleteWebHook();
            });
        }
    }

    public record class MessageRequestDTO(string ChatId, string Message);
}
