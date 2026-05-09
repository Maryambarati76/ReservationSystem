using AutoMapper;
using ReservationSystem.Core.DTOs;
using ReservationSystem.Core.Entities;
using ReservationSystem.Core.Enums;
using ReservationSystem.Core.Interfaces;
using ReservationSystem.Core.Services;

namespace ReservationSystem.Infrastructure.Services;

public class ReservationService : IReservationService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public ReservationService(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<ReservationResponseDto> CreateReservationAsync(ReservationCreateDto dto)
    {
        // ۱. چک کردن مدت زمان (حداکثر ۴ ساعت)
        var duration = dto.EndTime - dto.StartTime;
        if (duration.TotalHours > 4)
            throw new Exception("مدت زمان رزرو نمی‌تواند بیش از ۴ ساعت باشد.");

        // ۲. چک کردن تعداد رزروهای فعال کاربر (حداکثر ۲ رزرو)
        var userActiveReservations = await _unitOfWork.Reservations.GetActiveReservationsByUserAsync(dto.UserId);
        if (userActiveReservations.Count() >= 2)
            throw new Exception("هر کاربر حداکثر می‌تواند ۲ رزرو فعال داشته باشد.");

        // ۳. چک کردن تداخل زمانی (Overlap)
        var overlaps = await _unitOfWork.Reservations.GetActiveReservationsForResourceAsync(dto.ResourceId, dto.StartTime, dto.EndTime);
        if (overlaps.Any())
            throw new Exception("این منبع در بازه زمانی انتخاب شده قبلاً رزرو شده است.");

        // ایجاد رزرو
        var reservation = _mapper.Map<Reservation>(dto);
        reservation.Status = ReservationStatus.Active;

        await _unitOfWork.Reservations.AddAsync(reservation);
        await _unitOfWork.CompleteAsync();

        return _mapper.Map<ReservationResponseDto>(reservation);
    }

    public async Task<bool> CancelReservationAsync(int reservationId)
    {
        var reservation = await _unitOfWork.Reservations.GetByIdAsync(reservationId);
        if (reservation == null) return false;

        // فقط رزروهایی که شروع نشده‌اند قابل لغو هستند
        if (reservation.StartTime <= DateTime.Now)
            throw new Exception("امکان لغو رزروی که زمان آن شروع شده یا گذشته است وجود ندارد.");

        reservation.Status = ReservationStatus.Cancelled;
        await _unitOfWork.CompleteAsync();
        return true;
    }

    public async Task<IEnumerable<ReservationResponseDto>> GetResourceReservationsAsync(int resourceId, DateTime fromDate, DateTime toDate)
    {
        var reservations = await _unitOfWork.Reservations.GetReservationsByResourceAndDateRangeAsync(resourceId, fromDate, toDate);
        return _mapper.Map<IEnumerable<ReservationResponseDto>>(reservations);
    }

    public async Task<IEnumerable<ResourceDto>> GetAvailableResourcesTodayAsync()
    {
        // برای سادگی، تمام منابع را برمی‌گردانیم. 
        // در صورت نیاز به فیلتر دقیق‌تر برای "نوبت‌های آزاد"، منطق پیچیده‌تری لازم است.
        var resources = await _unitOfWork.Resources.GetAllAsync();
        return _mapper.Map<IEnumerable<ResourceDto>>(resources);
    }
}
