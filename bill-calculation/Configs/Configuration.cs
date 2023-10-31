namespace bill_calculation.Configuration
{
    public class Configuration
    {
        public Mongo Mongo { get; set; }

        public Telegram Telegram { get; set; }
    }

    public class Mongo
    {
        public string ConnectionString { get; set; }

        public string DbName { get; set; }
    }

    public class Telegram
    {
        public string BotToken { get; set; }

        public int ChatId { get; set; }
    }
}