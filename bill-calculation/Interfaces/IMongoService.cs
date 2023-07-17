using MongoDB.Driver;

public interface IMongoService
{
    MongoClient GetClient();

    IMongoDatabase GetDataBase();

    IMongoCollection<T> GetCollection<T>(MongoCollectionSettings settings = null) where T : BaseEntity;
}