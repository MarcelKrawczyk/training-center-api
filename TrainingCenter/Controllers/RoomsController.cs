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

        foreach (var room in DataStore.Rooms)
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
}