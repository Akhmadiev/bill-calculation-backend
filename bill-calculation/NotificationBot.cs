namespace bill_calculation
{

    using Microsoft.VisualBasic;

    using System.Collections;
    using System.Text;

    using Telegram.Bot;
    using Telegram.Bot.Types;

    public class NotificationBot
    {
        private readonly TelegramBotClient _client;
        private readonly ChatId _chatId;

        private readonly Configuration.Configuration _configuration;
        private readonly List<BotCommand> _botCommands;

        public NotificationBot(Configuration.Configuration configuration)
        {
            _configuration = configuration;
            _chatId = new ChatId(_configuration.Telegram.ChatId);
            _client = new TelegramBotClient(_configuration.Telegram.BotToken);

            _botCommands = new List<BotCommand>()
        {
            new BotCommand
            {
                Command = "get_all",
                Description = "Получить все данные очереди"
            },
            new BotCommand
            {
                Command = "get_last",
                Description = "Получить последние данные очереди"
            }
        };
        }

        public async Task Start()
        {
            //await _client.SetMyCommandsAsync(_botCommands);
            //var options = new ReceiverOptions { AllowedUpdates = { } };
            //await _client.ReceiveAsync(OnMessage, OnError, options);
        }

        public async Task SendMessage(string text)
        {
            await _client.SendTextMessageAsync(_chatId, $"```{text}```", null, Telegram.Bot.Types.Enums.ParseMode.Markdown);
        }

        private Task OnError(ITelegramBotClient client, Exception e, CancellationToken cToken)
        {
            throw new NotImplementedException();
        }

        private async Task OnMessage(ITelegramBotClient client, Update update, CancellationToken cToken)
        {
            if (update.Message?.EntityValues == null)
            {
                return;
            }

            foreach (var entity in update.Message.EntityValues)
            {
                //switch (entity)
                //{
                //    case "/get_all@unp_smev_bot":
                //        var allInfo = await _queueInfoService.GetAllSmevQueueInfo();
                //        var jsonString = Newtonsoft.Json.JsonConvert.SerializeObject(allInfo, Newtonsoft.Json.Formatting.Indented);
                //        var fileBytes = Encoding.UTF8.GetBytes(jsonString);
                //        using (var stream = new MemoryStream(fileBytes))
                //        {
                //            await _client.SendDocumentAsync(_chatId, InputFile.FromStream(stream, "data.json"));
                //        }
                //        return;
                //    case "/get_last@unp_smev_bot":
                //        var lastInfo = await _queueInfoService.GetLastSmevQueueInfo();
                //        jsonString = Newtonsoft.Json.JsonConvert.SerializeObject(lastInfo, Newtonsoft.Json.Formatting.Indented);
                //        await _client.SendTextMessageAsync(_chatId, $"```{jsonString?.Substring(0, jsonString.Length - 1)}```", null, Telegram.Bot.Types.Enums.ParseMode.Markdown);
                //        return;
                //    default:
                //        await _client.SendTextMessageAsync(_chatId, "Неизвестная команда!");
                //        return;
                //}
            }
        }
    }
}