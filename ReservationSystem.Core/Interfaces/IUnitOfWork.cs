namespace ReservationSystem.Core.Interfaces;

public interface IUnitOfWork
{
    IUserRepository Users { get; }
    IResourceRepository Resources { get; }
    IReservationRepository Reservations { get; }

    Task<int> CompleteAsync();
}