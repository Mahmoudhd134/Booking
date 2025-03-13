using Booking.Mvc.Services;
using Microsoft.AspNetCore.Mvc;

namespace Booking.Mvc.Controllers;

public class CustomersController(CustomerServices customerServices) : Controller
{
    public async Task<IActionResult> Index()
    {
        var customers = await customerServices.GetAll();
        return View(customers);
    }
}