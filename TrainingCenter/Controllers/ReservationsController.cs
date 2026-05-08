using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using TrainingCenter.Data;
using TrainingCenter.Models;

namespace TrainingCenter.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ReservationsController : ControllerBase
{
    //GET /api/reservations
    //GET /api/reservations?date=2026-05-10&status=confirmed&roomId=2
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
}