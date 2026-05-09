using Microsoft.EntityFrameworkCore;
using ReservationSystem.Core.Entities;
using ReservationSystem.Core.Enums;
using ReservationSystem.Core.Interfaces;
using ReservationSystem.Infrastructure.Persistence;

namespace ReservationSystem.Infrastructure.Repositories;

public class ReservationRepository : GenericRepository<Reservation>, IReservationRepository
{
    public ReservationRepository(ApplicationDbContext context) : base(context)
    {
    }

    public async Task<IEnumerable<Reservation>> GetReservationsByResourceAndDateRangeAsync(int resourceId, DateTime fromDate, DateTime toDate)
    {
        return await _context.Reservations
            .Include(r => r.User)
            .Include(r => r.Resource)
            .Where(r => r.ResourceId == resourceId &&
                        r.StartTime < toDate &&
                        r.EndTime > fromDate)
            .ToListAsync();
    }

    public async Task<IEnumerable<Reservation>> GetActiveReservationsByUserAsync(int userId)
    {
        return await _context.Reservations
            .Where(r => r.UserId == userId && r.Status == ReservationStatus.Active)
            .ToListAsync();
    }

    public async Task<IEnumerable<Reservation>> GetActiveReservationsForResourceAsync(int resourceId, DateTime startTime, DateTime endTime)
    {
        return await _context.Reservations
            .Where(r => r.ResourceId == resourceId &&
                        r.Status == ReservationStatus.Active &&
                        r.StartTime < endTime &&
                        r.EndTime > startTime)
            .ToListAsync();
    }
}