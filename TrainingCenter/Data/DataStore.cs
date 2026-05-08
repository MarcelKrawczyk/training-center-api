using TrainingCenter.Models;

namespace TrainingCenter.Data;

public static class DataStore
{
    public static List<Room> Rooms = new List<Room>
    {
        new Room
        {
            Id = 1, Name = "Lab 101", BuildingCode = "A", Floor = 1, Capacity = 20, HasProjector = true, IsActive = true
        },
        new Room
        {
            Id = 2, Name = "Lab 204", BuildingCode = "B", Floor = 2, Capacity = 24, HasProjector = true, IsActive = true
        },
        new Room
        {
            Id = 3, Name = "Room A", BuildingCode = "A", Floor = 0, Capacity = 40, HasProjector = false, IsActive = true
        },
        new Room
        {
            Id = 4, Name = "Room B", BuildingCode = "B", Floor = 1, Capacity = 15, HasProjector = true, IsActive = true
        },
        new Room
        {
            Id = 5, Name = "Room C", BuildingCode = "C", Floor = 0, Capacity = 5, HasProjector = false, IsActive = false
        },
    };

    public static List<Reservation> Reservations = new List<Reservation>
    {
        new Reservation
        {
            Id = 1, RoomId = 1, OrganizerName = "John S", Topic = "Zajęcia", Date = new DateTime(2026, 5, 10),
            StartTime = new TimeSpan(8, 0, 0), EndTime = new TimeSpan(10, 0, 0), Status = "confirmed"
        },
        new Reservation
        {
            Id = 2, RoomId = 1, OrganizerName = "Mary L", Topic = "REST API brainstorm meeting",
            Date = new DateTime(2026, 5, 10), StartTime = new TimeSpan(10, 30, 0), EndTime = new TimeSpan(12, 30, 0),
            Status = "planned"
        },
        new Reservation
        {
            Id = 3, RoomId = 2, OrganizerName = "Ann I", Topic = "Zajęcia HTTP", Date = new DateTime(2026, 5, 11),
            StartTime = new TimeSpan(9, 0, 0), EndTime = new TimeSpan(11, 0, 0), Status = "confirmed"
        },
        new Reservation
        {
            Id = 4, RoomId = 3, OrganizerName = "Peter K", Topic = "Meeting", Date = new DateTime(2026, 5, 12),
            StartTime = new TimeSpan(13, 0, 0), EndTime = new TimeSpan(15, 0, 0), Status = "planned"
        },
        new Reservation
        {
            Id = 5, RoomId = 4, OrganizerName = "Kate S", Topic = "Szkolenia", Date = new DateTime(2026, 5, 13),
            StartTime = new TimeSpan(10, 0, 0), EndTime = new TimeSpan(12, 0, 0), Status = "cancelled"
        },
    };

    private static int _nextRoomId = Rooms.Max(r => r.Id) + 1;
    private static int _nextReservationId = Reservations.Max(r => r.Id) + 1;
}