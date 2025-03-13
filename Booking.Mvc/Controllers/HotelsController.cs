using Booking.Mvc.Services;
using Microsoft.AspNetCore.Mvc;

namespace Booking.Mvc.Controllers;

public class HotelsController(HotelService hotelService) : Controller
{
    public async Task<IActionResult> Index()
    {
        var hotels = await hotelService.GetHotelsAsync();
        return View(hotels);
    }
}