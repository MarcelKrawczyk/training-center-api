using Microsoft.AspNetCore.Mvc;
using TrainingCenter.Data;
using TrainingCenter.Models;

namespace TrainingCenter.Controllers;

[ApiController]
[Route("api/[controller]")]
public class RoomsController : ControllerBase
{
    [HttpGet]
    public IActionResult GetAll(
        [FromQuery] int? minCapacity = null,
        [FromQuery] bool? hasProjector = null,
        [FromQuery] bool? activeOnly = null)
    {
        var result = new List<Room>();

        foreach (var room in AppData.Rooms)
        {
            if (minCapacity != null && room.Capacity < minCapacity)
                continue;

            if (hasProjector != null && room.HasProjector != hasProjector)
                continue;

            if (activeOnly == true && room.IsActive == false)
                continue;

            result.Add(room);
        }
        return Ok(result);
    }

    [HttpGet("{id}")]
    public IActionResult GetById(int id)
    {
        Room? result = null;

        foreach (var room in AppData.Rooms)
        {
            if (room.Id == id)
            {
                result = room;
            }
        }
        if (result == null)
        {
            return NotFound("Room not found");
        }
        return Ok(result);
    }

    [HttpGet("building/{buildingCode}")]
    public IActionResult GetByBuildingCode(string buildingCode)
    {
        var result = new List<Room>();

        foreach (var room in AppData.Rooms)
        {
            if (room.BuildingCode == buildingCode)
            {
                result.Add(room);
            }
        }
        return Ok(result);
    }
    
    [HttpPost]
    public IActionResult Create([FromBody] Room room)
    {
        room.Id = AppData.NextRoomId;
        
        AppData.Rooms.Add(room);

        return CreatedAtAction("GetById", new { id = room.Id }, room);
    }

    [HttpPut("{id}")]
    public IActionResult Update(int id, [FromBody] Room updated)
    {
        Room? result = null;
        foreach (var room in AppData.Rooms)
        {
            if (room.Id == id)
            {
                result = room;
                break;
            }
        }
        if (result == null)
            return NotFound("Room not found");
        
        result.Name = updated.Name;
        result.Capacity = updated.Capacity;
        result.IsActive = updated.IsActive;
        result.BuildingCode = updated.BuildingCode;
        result.Floor = updated.Floor;
        result.HasProjector = updated.HasProjector;
        
        return Ok(result);
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        Room? deletedRoom = null;
        foreach (var room in AppData.Rooms)
        {
            if (room.Id == id)
            {
                deletedRoom = room;
                break;
            }
        }
        if (deletedRoom == null)
        {
            return NotFound("Room not found");
        }

        foreach (var reservation in AppData.Reservations)
        {
            var reservationStart = reservation.Date + reservation.StartTime;
            if (reservation.RoomId == id && reservationStart >= DateTime.Now)
            {
                return Conflict("Cannot delete Room that has a upcoming reservation");
            }
        }
        AppData.Rooms.Remove(deletedRoom);
        return NoContent();
    }
}