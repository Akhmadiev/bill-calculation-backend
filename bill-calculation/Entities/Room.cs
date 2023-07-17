public class Room : BaseEntity
{
    public override string CollectionName => "Rooms";

    public string Name { get; set; }

    public decimal Price { get; set; }
}