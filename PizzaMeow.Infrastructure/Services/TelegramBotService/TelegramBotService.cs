using PizzaMeow.GoogleMaps;
using System.Text;
using System.Text.Json;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

namespace PizzaMeow.TelegramBotService
{
    public class TelegramBotService
    {
        private readonly TelegramBotClient _botclient;
        private readonly HttpClient _httpClient;
        private readonly string _token;

        public TelegramBotService(string token, HttpClient httpClient)
        {
            _token = token;
            _botclient = new TelegramBotClient(token);
            _httpClient = httpClient;
        }

        public async Task SendMessageAsync(string chatId, string message) 
        {
            var url = $"https://api.telegram.org/bot{_token}/sendMessage";
            var payload = new
            {
                chat_id = chatId,
                text = message
            };

            var content = new StringContent(
                JsonSerializer.Serialize(payload),
                Encoding.UTF8,
                "application/json");

            var inlineKeyboard = new InlineKeyboardMarkup(new[]
            {
                InlineKeyboardButton.WithUrl("Accept", "")
            });

            var response = await _httpClient.PostAsync(url, content);
            response.EnsureSuccessStatusCode();
        }

        public async Task SendCoordinatesAsync(string chatId, (double, double) coordinates) 
        {
            var (latidude, lonidude) = coordinates;
            await _botclient.SendLocation(
                chatId: chatId,
                latitude: latidude,
                longitude: lonidude
                );
        }

        //TODO
        public async Task GetMessageId(string chatId)
        {
            throw new NotImplementedException();
        }

        //TODO
        public async Task EditMessageAsync(string chatId, string messageId)
        {
            throw new NotImplementedException();
        }

        public async Task SetWebHookAsync(string url)
        {
            await _botclient.SetWebhook(url);
        }

        public async Task DeleteWebHook()
        {
            await _botclient.DeleteWebhook();
        }
    }
}
