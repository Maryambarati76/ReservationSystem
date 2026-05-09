namespace ReservationSystem.Core.DTOs;

public class ReservationCreateDto
{
    public int ResourceId { get; set; }
    public int UserId { get; set; }
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }
}