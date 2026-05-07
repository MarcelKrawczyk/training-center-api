namespace TrainingCenter.Models;

public class Reservation
{
    public int Id { get; set; }

    public int RoomId { get; set; }

    public String OrganizerName { get; set; }
    
    public String Topic  { get; set; }
    
    public DateTime Date { get; set; }
    
    public TimeSpan StartTime { get; set; }
    
    public TimeSpan EndTime { get; set; }
    
    public string Status { get; set; } = "planned";
}