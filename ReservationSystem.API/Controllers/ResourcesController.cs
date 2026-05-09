using Microsoft.AspNetCore.Mvc;
using ReservationSystem.Core.Services;

namespace ReservationSystem.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ResourcesController : ControllerBase
{
    private readonly IReservationService _reservationService;

    public ResourcesController(IReservationService reservationService)
    {
        _reservationService = reservationService;
    }

    [HttpGet("available-today")]
    public async Task<IActionResult> GetAvailableToday()
    {
        var resources = await _reservationService.GetAvailableResourcesTodayAsync();
        return Ok(resources);
    }
}