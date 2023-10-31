using MongoDB.Driver;

public class MongoService : IMongoService
{
    private readonly IMongoDatabase _database;
    private readonly MongoClient _client;

    private readonly bill_calculation.Configuration.Configuration _configuration;

    public MongoService(bill_calculation.Configuration.Configuration configuration)
    {
        _configuration = configuration;
        _client ??= new MongoClient(_configuration.Mongo.ConnectionString);
        _database ??= _client.GetDatabase(_configuration.Mongo.DbName);
    }

    public MongoClient GetClient()
    {
        return _client;
    }

    public IMongoDatabase GetDataBase()
    {
        return _database;
    }

    public IMongoCollection<T> GetCollection<T>(MongoCollectionSettings settings = null) where T : BaseEntity
    {
        var collectionName = Activator.CreateInstance<T>().CollectionName;
        return _database.GetCollection<T>(collectionName, settings);
    }
}