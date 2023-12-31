﻿using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

using System.Text.RegularExpressions;
using System.Xml.Linq;

[ApiController]
[Route("[controller]")]
public class BillController : ControllerBase
{
    private readonly IBillService _billService;
    private readonly ITelegramService _telegramService;

    public BillController(BillService billService, ITelegramService telegramService)
    {
        _billService = billService;
        _telegramService = telegramService;
    }

    [HttpGet("GetRooms")]
    public async Task<List<Room>> GetRooms()
    {
        return await _billService.GetRooms();
    }

    [HttpGet("GetRoomGroups")]
    public async Task<List<Group>> GetRoomGroups(Guid roomId)
    {
        return await _billService.GetRoomGroups(roomId);
    }

    [HttpGet("GetRoomPersons")]
    public async Task<List<Person>> GetRoomPersons(Guid roomId)
    {
        return await _billService.GetRoomPersons(roomId);
    }

    [HttpGet("GetGroupPersons")]
    public async Task<List<Person>> GetGroupPersons(Guid groupId)
    {
        return await _billService.GetGroupPersons(groupId);
    }

    [HttpPost("CreateRoom")]
    public async Task<Room> CreateRoom(string name)
    {
        return await _billService.CreateRoom(name);
    }

    [HttpPost("AddGroup")]
    public async Task AddGroup(Guid roomId, string name)
    {
        await _billService.AddGroup(roomId, name);
    }

    [HttpPost("AddRoomPerson")]
    public async Task AddRoomPerson(Guid roomId, string name)
    {
        await _billService.AddRoomPerson(roomId, name);
    }

    [HttpPost("AddGroupPerson")]
    public async Task AddGroupPerson(Guid groupId, Guid personId)
    {
        await _billService.AddGroupPerson(groupId, personId);
    }

    [HttpPost("AddGroupPersons")]
    public async Task AddGroupPersons(Guid groupId, Guid[] personIds)
    {
        await _billService.AddGroupPersons(groupId, personIds);
    }


    [HttpPost("DeleteGroupPerson")]
    public async Task DeleteGroupPerson(Guid groupId, Guid personId)
    {
        await _billService.DeleteGroupPerson(groupId, personId);
    }

    [HttpPost("DeleteGroup")]
    public async Task DeleteGroup(Guid groupId)
    {
        await _billService.DeleteGroup(groupId);
    }

    [HttpPost("ChangeGroupCount")]
    public async Task ChangeGroupCount(Guid groupId, int count)
    {
        await _billService.ChangeGroupCount(groupId, count);
    }

    [HttpPost("ChangeGroupPrice")]
    public async Task ChangeGroupPrice(Guid groupId, decimal price)
    {
        await _billService.ChangeGroupPrice(groupId, price);
    }

    [HttpPost("ChangeGroupName")]
    public async Task ChangeGroupName(Guid groupId, string name)
    {
        await _billService.ChangeGroupName(groupId, name);
    }

    [HttpPost("CopyGroup")]
    public async Task CopyGroup(Guid groupId)
    {
        await _billService.CopyGroup(groupId);
    }

    [HttpGet("Calculate")]
    public async Task<string> Calculate(Guid roomId)
    {
        return await _billService.Calculate(roomId);
    }

    [HttpPost("SendToTelegram")]
    public async Task SendToTelegram([FromBody] string text)
    {
        await _telegramService.SendMessage(text);
    }
}