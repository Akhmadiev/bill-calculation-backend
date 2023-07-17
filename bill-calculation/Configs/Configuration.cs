public class Configuration
{
    public Mongo Mongo { get; set; }
}

public class Mongo
{
    public string ConnectionString { get; set; }

    public string DbName { get; set; }
}