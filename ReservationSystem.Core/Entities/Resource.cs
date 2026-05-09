namespace ReservationSystem.Core.Entities;

public class Resource
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Type { get; set; } = string.Empty; // مثلاً "Meeting Room" یا "Projector"
    public int MaxConcurrentUsage { get; set; } // ظرفیت استفاده همزمان

    // Navigation Property
    public ICollection<Reservation> Reservations { get; set; } = new List<Reservation>();
}