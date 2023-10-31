public interface ITelegramService
{
    Task SendMessage(string text);
}