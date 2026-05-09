using ReservationSystem.Core.Enums;

namespace ReservationSystem.Core.Entities;

public class Reservation
{
    public int Id { get; set; }
    public int ResourceId { get; set; }
    public int UserId { get; set; }
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }
    public ReservationStatus Status { get; set; }

    // Navigation Properties
    public Resource Resource { get; set; } = null!;
    public User User { get; set; } = null!;
}