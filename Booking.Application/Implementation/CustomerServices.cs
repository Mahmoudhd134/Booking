using Booking.Application.Dtos.Book;
using Booking.Application.Services;
using Booking.DataAccess.Data;
using Booking.Domain.ErrorHandling;
using Booking.Domain.ErrorHandling.Errors;
using Booking.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Booking.Application.Implementation;

public class CustomerServices(BookingDbContext context) : ICustomerServices
{
    public async Task<Result<string>> Add(string nId, string name, string phone,
        CancellationToken cancellationToken = default)
    {
        var exists = await context.Customers.AnyAsync(c => c.NId == nId, cancellationToken);
        if (exists)
        {
            return CustomerErrors.NIdExists;
        }

        var customerResult = Customer.Create(nId, name, phone);
        if (customerResult.IsFailure)
            return customerResult.Error;
        context.Customers.Add(customerResult.Data);
        await context.SaveChangesAsync(cancellationToken);
        return customerResult.Data.NId;
    }

    public async Task<Result<List<CustomerDto>>> All(CancellationToken cancellationToken = default)
    {
        var customerList = await context.Customers
            .Select(x => new CustomerDto()
            {
                NId = x.NId,
                PhoneNumber = x.PhoneNumber,
                Name = x.Name,
            })
            .ToListAsync(cancellationToken);
        return customerList;
    }
}