using System.ComponentModel;

public class Group : BaseEntity
{
    public override string CollectionName => "Groups";

    public Room Room { get; set; }

    public string Name { get; set; }

    public decimal Price { get; set; }

    [DefaultValue(1)]
    public int Count { get; set; }

    public List<Person> Persons { get; set; }
}