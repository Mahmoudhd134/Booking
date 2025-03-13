using Booking.Mvc.Models.Booking;
using Booking.Mvc.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Booking.Mvc.Controllers;

public class BookingController(
    BookingService bookingService,
    HotelService hotelService,
    CustomerServices customerServices) : Controller
{
    public async Task<IActionResult> Index(int page = 1, int pageSize = 20)
    {
        try
        {
            var bookingResponse = await bookingService.GetBookingsAsync(page, pageSize);
            return View(bookingResponse);
        }
        catch
        {
            return View("Error");
        }
    }

    public async Task<IActionResult> Details(int id)
    {
        try
        {
            var booking = await bookingService.GetBookingByIdAsync(id);
            return View(booking);
        }
        catch
        {
            return View("Error");
        }
    }

    public async Task<IActionResult> Add()
    {
        var hotels = await hotelService.GetHotelsAsync();
        var customers = await customerServices.GetAll();
        var model = new AddBookingViewModel
        {
            Hotels = hotels,
            Customers = customers
        };
        return View(model);
    }
}

public class AddBookingViewModel
{
    public List<HotelModel> Hotels { get; set; }
    public List<CustomerModel> Customers { get; set; }
}