using Telegram.Bot;
using Telegram.Bot.Polling;
using Telegram.Bot.Types.Enums;

namespace CouriersBot.TelegramBotService;

public class TelegramBotService
{
    public ITelegramBotClient botClient;
    public ReceiverOptions receiverOptions;

    public TelegramBotService()
    {
        botClient = new TelegramBotClient("7871029034:AAHkFzCmENIgreKd5UUL7MrowKAdutmZzt8");
        receiverOptions = new ReceiverOptions
        {
            
        };
    }
}
