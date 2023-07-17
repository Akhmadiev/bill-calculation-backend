using MongoDB.Bson.Serialization.Attributes;

public abstract class BaseEntity
{
    private Guid id;

    private DateTime createDate;

    public abstract string CollectionName { get; }

    [BsonId]
    public Guid Id
    {
        get
        {
            return id;
        }

        set
        {
            if (value == Guid.Empty)
            {
                value = Guid.NewGuid();
            }

            id = value;
        }
    }

    public DateTime CreateDate
    {
        get
        {
            return createDate;
        }

        set
        {
            if (value == default)
            {
                value = DateTime.UtcNow.AddHours(3);
            }

            createDate = value;
        }
    }
}