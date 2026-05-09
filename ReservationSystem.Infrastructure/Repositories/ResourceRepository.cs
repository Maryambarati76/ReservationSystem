using ReservationSystem.Core.Entities;
using ReservationSystem.Core.Interfaces;
using ReservationSystem.Infrastructure.Persistence;

namespace ReservationSystem.Infrastructure.Repositories;

public class ResourceRepository : GenericRepository<Resource>, IResourceRepository
{
    public ResourceRepository(ApplicationDbContext context) : base(context)
    {
    }
}