using Booking.Application.Dtos.BookItem;

namespace Booking.Api.Dtos;

public record AddBookDto(
    string CustomerNId,
    int HotelId,
    List<AddBookItemDto> Items,
    DateTime CheckIn,
    DateTime CheckOut
);