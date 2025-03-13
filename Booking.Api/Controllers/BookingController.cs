using Booking.Api.Dtos;
using Booking.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace Booking.Api.Controllers;

public class BookingController(IBookServices bookServices) : BaseController
{
    [HttpPost]
    public async Task<IResult> Add(AddBookDto dto)
    {
        var result = await bookServices.Add(dto.CustomerNId, dto.HotelId, dto.Items, dto.CheckIn, dto.CheckOut);
        return result.IsFailure ? Results.BadRequest(result.Error) : Results.Created();
    }
    
    [HttpPost("price")]
    public async Task<IResult> GetPrice(AddBookDto dto)
    {
        var result = await bookServices.GetPrice(dto.CustomerNId, dto.HotelId, dto.Items, dto.CheckIn, dto.CheckOut);
        return result.IsFailure ? Results.BadRequest(result.Error) : Results.Ok(result.Data);
    }

    [HttpGet("{id:int}")]
    public async Task<IResult> Get(int id)
    {
        var result = await bookServices.GetById(id);
        return result.IsFailure ? Results.BadRequest(result.Error) : Results.Ok(result.Data);
    }

    [HttpGet]
    public async Task<IResult> GetAll(int page = 1, int pageSize = 10)
    {
        var result = await bookServices.All(page, pageSize);
        return result.IsFailure ? Results.BadRequest(result.Error) : Results.Ok(result.Data);
    }
    
}