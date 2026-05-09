using ReservationSystem.Core.DTOs;

namespace ReservationSystem.Core.Services;

public interface IReservationService
{
    Task<ReservationResponseDto> CreateReservationAsync(ReservationCreateDto dto);
    Task<bool> CancelReservationAsync(int reservationId);
    Task<IEnumerable<ReservationResponseDto>> GetResourceReservationsAsync(int resourceId, DateTime fromDate, DateTime toDate);
    Task<IEnumerable<ResourceDto>> GetAvailableResourcesTodayAsync();
}