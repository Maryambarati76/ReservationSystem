using ReservationSystem.Core.Entities;

namespace ReservationSystem.Core.Interfaces;

public interface IUserRepository : IGenericRepository<User>
{
    Task<User?> GetByEmailAsync(string email);
}