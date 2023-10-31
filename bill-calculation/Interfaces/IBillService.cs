using Microsoft.AspNetCore.Mvc;
/// <summary>
/// Сервис расчета чека
/// </summary>
public interface IBillService
{
    /// <summary>
    /// Добавить группу
    /// </summary>
    Task AddGroup(Guid roomId, string name);

    /// <summary>
    /// Добавить персону в группу
    /// </summary>
    Task AddGroupPerson(Guid groupId, Guid personId);

    /// <summary>
    /// Добавить персон в группу
    /// </summary>
    Task AddGroupPersons(Guid groupId, Guid[] personIds);

    /// <summary>
    /// Добавить персону в комнату
    /// </summary>
    Task AddRoomPerson(Guid roomId, string name);

    /// <summary>
    /// Рассчет чека
    /// </summary>
    Task<string> Calculate(Guid roomId);

    /// <summary>
    /// Поменять количество группы
    /// </summary>
    Task ChangeGroupCount(Guid groupId, int count);

    /// <summary>
    /// Поменять цену группы
    /// </summary>
    Task ChangeGroupPrice(Guid groupId, decimal price);

    /// <summary>
    /// Поменять имя группы
    /// </summary>
    Task ChangeGroupName(Guid groupId, string name);

    /// <summary>
    /// Скопировать группу
    /// </summary>
    Task CopyGroup(Guid groupId);

    /// <summary>
    /// Создать комнату
    /// </summary>
    Task<Room> CreateRoom(string name);

    /// <summary>
    /// Удалить группу
    /// </summary>
    Task DeleteGroup(Guid groupId);

    /// <summary>
    /// Удалить персону из группы
    /// </summary>
    Task DeleteGroupPerson(Guid groupId, Guid personId);

    /// <summary>
    /// Получить персон в группе
    /// </summary>
    Task<List<Person>> GetGroupPersons(Guid groupId);

    /// <summary>
    /// Получить группы
    /// </summary>
    Task<List<Group>> GetRoomGroups(Guid roomId);

    /// <summary>
    /// Получить персоны
    /// </summary>
    Task<List<Person>> GetRoomPersons(Guid roomId);

    /// <summary>
    /// Получить комнаты
    /// </summary>
    Task<List<Room>> GetRooms();
}