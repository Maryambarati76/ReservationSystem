using FluentValidation;
using ReservationSystem.Core.DTOs;

namespace ReservationSystem.Core.Validators;

public class ReservationCreateDtoValidator : AbstractValidator<ReservationCreateDto>
{
    public ReservationCreateDtoValidator()
    {
        RuleFor(x => x.ResourceId)
            .GreaterThan(0)
            .WithMessage("شناسه منبع الزامی است.");

        RuleFor(x => x.UserId)
            .GreaterThan(0)
            .WithMessage("شناسه کاربر الزامی است.");

        RuleFor(x => x.StartTime)
            .NotEmpty()
            .WithMessage("زمان شروع الزامی است.")
            .GreaterThan(DateTime.Now)
            .WithMessage("زمان شروع نمی‌تواند در گذشته باشد.");

        RuleFor(x => x.EndTime)
            .NotEmpty()
            .WithMessage("زمان پایان الزامی است.")
            .GreaterThan(x => x.StartTime)
            .WithMessage("زمان پایان باید بعد از زمان شروع باشد.");
    }
}