using Booking.Application.Dtos.Hotel;
using Booking.Application.Services;
using Booking.DataAccess.Data;
using Microsoft.AspNetCore.Mvc;

namespace Booking.Api.Controllers;

public class HotelController(IHotelServices hotelServices) : BaseController
{
    [HttpPost]
    public async Task<IResult> Add(AddHotelDto dto)
    {
        var result = await hotelServices.Add(dto);
        if (result.IsFailure)
            return Results.BadRequest(result.Error);
        return Results.Created();
    }

    [HttpGet("{id:int}/available-rooms")]
    public async Task<IResult> AvailableRooms(int id, DateTime checkIn, DateTime checkOut)
    {
        var result = await hotelServices.GetAvailableRoomsForHotelInPeriod(id, checkIn, checkOut);
        if (result.IsFailure)
            return Results.BadRequest(result.Error);
        return Results.Ok(result.Data);
    }

    [HttpGet]
    public async Task<IResult> All()
    {
        var result = await hotelServices.All();
        return result.IsFailure ? Results.BadRequest(result.Error) : Results.Ok(result.Data);
    }
}