using ReservationSystem.Core.Interfaces;
using ReservationSystem.Infrastructure.Persistence;

namespace ReservationSystem.Infrastructure.Repositories;

public class UnitOfWork : IUnitOfWork
{
    private readonly ApplicationDbContext _context;

    public IUserRepository Users { get; }
    public IResourceRepository Resources { get; }
    public IReservationRepository Reservations { get; }

    public UnitOfWork(ApplicationDbContext context)
    {
        _context = context;
        Users = new UserRepository(context);
        Resources = new ResourceRepository(context);
        Reservations = new ReservationRepository(context);
    }

    public async Task<int> CompleteAsync()
    {
        return await _context.SaveChangesAsync();
    }
}