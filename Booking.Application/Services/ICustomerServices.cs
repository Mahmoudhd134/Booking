using Booking.Application.Dtos.Book;
using Booking.Domain.ErrorHandling;

namespace Booking.Application.Services;

public interface ICustomerServices
{
    Task<Result<string>> Add(string nId, string name, string phone, CancellationToken cancellationToken = default);
    Task<Result<List<CustomerDto>>> All(CancellationToken cancellationToken = default);
}