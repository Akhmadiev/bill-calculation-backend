public class Person : BaseEntity
{
    public override string CollectionName => "Persons";

    public Room Room { get; set; }

    public string Name { get; set; }
}