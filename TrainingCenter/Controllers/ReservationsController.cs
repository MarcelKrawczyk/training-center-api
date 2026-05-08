using Microsoft.AspNetCore.Mvc;
using TrainingCenter.Data;
using TrainingCenter.Models;

namespace TrainingCenter.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ReservationsController : ControllerBase
{
    [HttpGet]
    public IActionResult GetAll(
        [FromQuery] DateTime? date = null,
        [FromQuery] string? status = null,
        [FromQuery] int? roomId = null)
    {
        var result = new List <Reservation>();

        foreach (var res in DataStore.Reservations)
        {
            if(date != null && res.Date != date)
                continue;
            
            if(status != null && res.Status != status)
                continue;
            
            if(roomId != null && res.RoomId != roomId)
                continue;
            
            result.Add(res);
        }
        return Ok(result);
    }

    [HttpGet("{id}")]
    public IActionResult GetById(int id)
    {
        Reservation? result = null;

        foreach (var res in DataStore.Reservations)
        {
            if (res.Id == id)
            {
                result = res;
            }
        }
        if (result == null)
        {
           return NotFound("Reservation not found"); 
        }
        return Ok(result);
    }

    [HttpPost]
    public IActionResult Create([FromBody] Reservation reservation)
    {
        if (reservation.EndTime <= reservation.StartTime)
            return BadRequest("EndTime must be after the StartTime");
        
        var room = DataStore.Rooms.FirstOrDefault(r => r.Id == reservation.RoomId);
 
        if (room == null)
            return NotFound("Room not found");
        
        if (!room.IsActive)
            return BadRequest("Room is not active");
        
        bool hasConflict = DataStore.Reservations.Any(r =>
            r.RoomId == reservation.RoomId &&
            r.Status != "cancelled" &&
            r.Date.Date == reservation.Date.Date &&
            r.StartTime < reservation.EndTime &&
            reservation.StartTime < r.EndTime);
        
        if (hasConflict)
            return Conflict("Room is reserved in the scheduled time");
        
        reservation.Id = DataStore.nextReservationId;
        DataStore.Reservations.Add(reservation);
        
        return CreatedAtAction("GetById", new { id = reservation.Id }, reservation);
    }

    [HttpPut("{id}")]
    public IActionResult Update(int id, [FromBody] Reservation reservation)
    {
        if (reservation.EndTime <= reservation.StartTime)
            return BadRequest("EndTime must be after the StartTime");
 
        var existing = DataStore.Reservations.FirstOrDefault(r => r.Id == id);
 
        if (existing == null)
            return NotFound("Reservation not found");
 
        var room = DataStore.Rooms.FirstOrDefault(r => r.Id == reservation.RoomId);
 
        if (room == null)
            return NotFound("Room not found");
 
        if (!room.IsActive)
            return BadRequest("The room is not active");
        
        bool hasConflict = DataStore.Reservations.Any(r =>
            r.Id != id &&
            r.RoomId == reservation.RoomId &&
            r.Status != "cancelled" &&
            r.Date.Date == reservation.Date.Date &&
            r.StartTime < reservation.EndTime &&
            reservation.StartTime < r.EndTime);
        if (hasConflict)
            return Conflict("Room is reserved in the scheduled time");
        
        existing.RoomId = reservation.RoomId;
        existing.OrganizerName = reservation.OrganizerName;
        existing.Topic = reservation.Topic;
        existing.Date = reservation.Date;
        existing.StartTime = reservation.StartTime;
        existing.EndTime = reservation.EndTime;
        existing.Status = reservation.Status;
 
        return Ok(existing);
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        var reservation = DataStore.Reservations.FirstOrDefault(r => r.Id == id);

        if (reservation == null)
            return NotFound("Reservation not found");
        
        DataStore.Reservations.Remove(reservation);
        return NoContent();
    }
}