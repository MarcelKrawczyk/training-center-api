using System.ComponentModel.DataAnnotations;

namespace TrainingCenter.Models;

public class Reservation
{
    public int Id { get; set; }

    public int RoomId { get; set; }
    
    [Required]
    public String OrganizerName { get; set; }
    
    [Required]
    public String Topic  { get; set; }
    
    public DateTime Date { get; set; }
    
    public TimeSpan StartTime { get; set; }
    
    public TimeSpan EndTime { get; set; }
    
    [Required]
    [RegularExpression("planned|confirmed|cancelled",
        ErrorMessage = "Status needs to be planned, confirmed or cancelled.")]
    public string Status { get; set; } = "planned";
}