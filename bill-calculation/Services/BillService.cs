using bill_calculation.Models;

using MongoDB.Driver;

public class BillService
{
    private List<string> defPersons = new List<string> { 
        "Коля",
        "Саша",
        "Нияз",
        "Марат",
        "Булат",
        "Рамиль",
        "Рустам"
    };

    private List<string> defGroups = new List<string> {
        "Кальян",
        "Лимонад",
        "Чай"
    };

    private readonly IMongoService _mongoService;

    public BillService(IMongoService mongoService)
    {
        _mongoService = mongoService;
    }

    public async Task<Room> CreateRoom(string name)
    {
        var rooms = _mongoService.GetCollection<Room>();
        var room = new Room { Name = name };

        await rooms.InsertOneAsync(room);

        var persons = _mongoService.GetCollection<Person>();

        foreach (var defPerson in defPersons)
        {
            var person = new Person { Room = room, Name = defPerson };
            await persons.InsertOneAsync(person);
        }

        var groups = _mongoService.GetCollection<Group>();

        foreach (var defGroup in defGroups)
        {
            var person = new Group { Room = room, Name = defGroup, Count = 1 };
            await groups.InsertOneAsync(person);
        }

        return room;
    }

    public async Task AddGroup(Guid roomId, string name)
    {
        var rooms = _mongoService.GetCollection<Room>();
        var room = await rooms.Find(x => x.Id == roomId).FirstOrDefaultAsync();
        var groups = _mongoService.GetCollection<Group>();

        var group = new Group { Room = room, Name = name, Count = 1 };
        
        await groups.InsertOneAsync(group);
    }

    public async Task AddRoomPerson(Guid roomId, string name)
    {
        var rooms = _mongoService.GetCollection<Room>();
        var room = await rooms.Find(x => x.Id == roomId).FirstOrDefaultAsync();
        var persons = _mongoService.GetCollection<Person>();

        var person = new Person { Room = room, Name = name };

        await persons.InsertOneAsync(person);
    }

    public async Task AddGroupPerson(Guid groupId, Guid personId)
    {
        var groups = _mongoService.GetCollection<Group>();
        var group = await groups.Find(x => x.Id == groupId).FirstOrDefaultAsync();
        var persons = _mongoService.GetCollection<Person>();
        var person = await persons.Find(x => x.Id == personId).FirstOrDefaultAsync();

        if (group.Persons == null)
        {
            group.Persons = new List<Person>();
        }

        if (group.Persons.Any(x => x.Id == personId))
        {
            return;
        }

        group.Persons.Add(person);

        var filter = Builders<Group>.Filter.Eq(x => x.Id, groupId);
        var update = Builders<Group>.Update.Set(x => x.Persons, group.Persons);

        await groups.UpdateOneAsync(filter, update);
    }

    public async Task DeleteGroupPerson(Guid groupId, Guid personId)
    {
        var groups = _mongoService.GetCollection<Group>();
        var group = await groups.Find(x => x.Id == groupId).FirstOrDefaultAsync();
        var persons = _mongoService.GetCollection<Person>();
        var person = await persons.Find(x => x.Id == personId).FirstOrDefaultAsync();

        group.Persons = group.Persons.Where(x => x.Id != personId).ToList();

        var filter = Builders<Group>.Filter.Eq(x => x.Id, groupId);
        var update = Builders<Group>.Update.Set(x => x.Persons, group.Persons);

        await groups.UpdateOneAsync(filter, update);
    }

    public async Task DeleteGroup(Guid groupId)
    {
        var groups = _mongoService.GetCollection<Group>();
        var group = await groups.Find(x => x.Id == groupId).FirstOrDefaultAsync();
        var filter = Builders<Group>.Filter.Eq(x => x.Id, groupId);
        await groups.DeleteOneAsync(filter);
    }

    public async Task ChangeGroupCount(Guid groupId, int count)
    {
        var groups = _mongoService.GetCollection<Group>();
        var group = await groups.Find(x => x.Id == groupId).FirstOrDefaultAsync();

        var filter = Builders<Group>.Filter.Eq(x => x.Id, groupId);
        var update = Builders<Group>.Update.Set(x => x.Count, count);

        await groups.UpdateOneAsync(filter, update);
    }

    public async Task ChangeGroupPrice(Guid groupId, decimal price)
    {
        var groups = _mongoService.GetCollection<Group>();
        var group = await groups.Find(x => x.Id == groupId).FirstOrDefaultAsync();

        var filter = Builders<Group>.Filter.Eq(x => x.Id, groupId);
        var update = Builders<Group>.Update.Set(x => x.Price, price);

        await groups.UpdateOneAsync(filter, update);
    }

    public async Task<List<Room>> GetRooms()
    {
        var data = await _mongoService.GetCollection<Room>().Find(x => true).ToListAsync();
        return data;
    }

    public async Task<List<Group>> GetRoomGroups(Guid roomId)
    {
        var data = await _mongoService.GetCollection<Group>().Find(x => x.Room.Id == roomId).ToListAsync();
        return data;
    }

    public async Task<List<Person>> GetRoomPersons(Guid roomId)
    {
        var data = await _mongoService.GetCollection<Person>().Find(x => x.Room.Id == roomId).ToListAsync();
        return data;
    }

    public async Task<List<Person>> GetGroupPersons(Guid groupId)
    {
        var data = await _mongoService.GetCollection<Group>().Find(x => x.Id == groupId).FirstOrDefaultAsync();
        return data.Persons;
    }

    public async Task<string> Calculate(Guid roomId)
    {
        var groups = await _mongoService.GetCollection<Group>()
            .Find(x => x.Room.Id == roomId && x.Count > 0 && x.Persons != null && x.Persons.Count() > 0)
            .ToListAsync();

        var personCalculations = new List<PersonCalculation>();
        var result = "";
        var groupResult = "";

        if (!groups.Any())
        {
            return "Группа не может быть пустой!";
        }

        foreach (var group in groups)
        {
            groupResult += $"{group.Name}({group.Price}₽) * {group.Count}шт = {group.Price * group.Count}₽ / {group.Persons.Count}чел = {Math.Round(group.Price * group.Count / group.Persons.Count, 2)}₽ ({string.Join(", ", group.Persons.Select(x => x.Name))})\n";

            foreach (var person in group.Persons)
            {
                var personCalculation = personCalculations.FirstOrDefault(x => x.PersonId == person.Id);

                if (personCalculation == null)
                {
                    personCalculation = new PersonCalculation
                    {
                        PersonId = person.Id,
                        PersonName = person.Name,
                        PersonCalculationGroups = new List<PersonCalculationGroup>()
                    };
                    personCalculations.Add(personCalculation);
                }

                personCalculation.PersonCalculationGroups.Add(new PersonCalculationGroup
                {
                    GroupId = group.Id,
                    GroupName = group.Name,
                    GroupCount = group.Count,
                    GroupPrice = group.Price * group.Count,
                    PersonPrice = Math.Round(group.Price * group.Count / group.Persons.Count(), 2)
                });
            }
        }

        result += $"{groups[0].Room.Name}: {Math.Round(personCalculations.SelectMany(x => x.PersonCalculationGroups).Sum(x => x.PersonPrice))}₽\n\n";
        result += groupResult + "\n";

        foreach (var personCalculation in personCalculations.OrderBy(x => x.PersonCalculationGroups.Sum(y => y.PersonPrice)))
        {
            personCalculation.TotalPrice = personCalculation.PersonCalculationGroups.Sum(x => x.PersonPrice);

            result += $"{personCalculation.PersonName}: ";

            foreach (var group in personCalculation.PersonCalculationGroups)
            {
                result += $"{group.PersonPrice} + ";
            }

            result = result.Substring(0, result.Length - 2);
            result += $"= {personCalculation.TotalPrice}\n";
        }

        return result;
    }
}