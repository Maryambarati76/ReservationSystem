using Microsoft.EntityFrameworkCore;
using ReservationSystem.Core.Entities;
using ReservationSystem.Core.Interfaces;
using ReservationSystem.Infrastructure.Persistence;

namespace ReservationSystem.Infrastructure.Repositories;

public class UserRepository : GenericRepository<User>, IUserRepository
{
    public UserRepository(ApplicationDbContext context) : base(context)
    {
    }

    public async Task<User?> GetByEmailAsync(string email)
    {
        return await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
    }
}