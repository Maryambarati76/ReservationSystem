using ReservationSystem.Core.Entities;

namespace ReservationSystem.Core.Interfaces;

public interface IReservationRepository : IGenericRepository<Reservation>
{
    Task<IEnumerable<Reservation>> GetReservationsByResourceAndDateRangeAsync(int resourceId, DateTime fromDate, DateTime toDate);
    Task<IEnumerable<Reservation>> GetActiveReservationsByUserAsync(int userId);
    Task<IEnumerable<Reservation>> GetActiveReservationsForResourceAsync(int resourceId, DateTime startTime, DateTime endTime);
}