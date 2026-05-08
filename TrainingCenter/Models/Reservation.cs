using System.ComponentModel.DataAnnotations;

namespace TrainingCenter.Models;

public class Reservation
{
    public int Id { get; set; }

    public int RoomId { get; set; }

    [Required] 
    public string OrganizerName { get; set; } = string.Empty;

    [Required] 
    public string Topic { get; set; } = string.Empty;
    
    public DateTime Date { get; set; }
    
    public TimeSpan StartTime { get; set; }
    
    public TimeSpan EndTime { get; set; }
    
    [Required]
    [RegularExpression("planned|confirmed|cancelled",
        ErrorMessage = "Status needs to be planned, confirmed or cancelled.")]
    public string Status { get; set; } = "planned";
}