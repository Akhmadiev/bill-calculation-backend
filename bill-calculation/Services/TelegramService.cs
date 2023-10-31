using Telegram.Bot;
using Telegram.Bot.Types;

namespace bill_calculation.Services
{
    public class TelegramService : ITelegramService
    {
        private readonly TelegramBotClient _client;
        private readonly ChatId _chatId;

        public TelegramService(Configuration.Configuration config)
        {
            _chatId = new ChatId(config.Telegram.ChatId);
            _client = new TelegramBotClient(config.Telegram.BotToken);
        }

        public async Task SendMessage(string text)
        {
            await _client.SendTextMessageAsync(_chatId, $"<i>{text}</i>", null, Telegram.Bot.Types.Enums.ParseMode.Html);
        }
    }
}
