using Booking.Api.Dtos;
using Booking.Application.Services;
using Booking.Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace Booking.Api.Controllers;

public class CustomerController(ICustomerServices customerServices) : BaseController
{
    [HttpPost]
    public async Task<IResult> CreateCustomer(AddCustomerDto dto)
    {
        var result = await customerServices.Add(dto.NId, dto.Name, dto.Phone);
        return result.IsFailure ? Results.BadRequest(result.Error) : Results.Created();
    }

    [HttpGet]
    public async Task<IResult> GetAllCustomers()
    {
        var result = await customerServices.All();
        return result.IsFailure ? Results.BadRequest(result.Error) : Results.Ok(result.Data);
    }
}