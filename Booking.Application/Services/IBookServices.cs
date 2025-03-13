using Booking.Application.Dtos;
using Booking.Application.Dtos.Book;
using Booking.Application.Dtos.BookItem;
using Booking.Domain.ErrorHandling;

namespace Booking.Application.Services;

public interface IBookServices
{
    Task<Result<int>> Add(string customerId, int hotelId, List<AddBookItemDto> items, DateTime checkIn,
        DateTime checkOut,
        CancellationToken cancellationToken = default);

    Task<Result<BookPrice>> GetPrice(string customerId, int hotelId, List<AddBookItemDto> items, DateTime checkIn,
        DateTime checkOut,
        CancellationToken cancellationToken = default);


    Task<Result<PageDto<BookForListDto>>> All(int page = 1, int pageSize = 10);
    Task<Result<BookDto>> GetById(int id, CancellationToken cancellationToken = default);
}
