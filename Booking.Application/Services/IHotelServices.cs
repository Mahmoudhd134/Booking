using Booking.Application.Dtos.Book;
using Booking.Application.Dtos.Hotel;
using Booking.Domain.ErrorHandling;

namespace Booking.Application.Services;

public interface IHotelServices
{
    public Task<Result<int>> Add(AddHotelDto model, CancellationToken cancellationToken = default);

    public Task<Result<List<HotelDto>>> All(CancellationToken cancellationToken = default);

    public Task<Result<List<RoomDto>>> GetAvailableRoomsForHotelInPeriod(int hotelId, DateTime checkIn,
        DateTime checkOut,
        CancellationToken cancellationToken = default);
}