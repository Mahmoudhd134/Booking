using Booking.Application.Dtos.Hotel;

namespace Booking.Application.Dtos.Book;

public class BookItemDto
{
    public RoomDto Room { get; set; }

    public int NumberOfAdults { get; set; }
    public int NumberOfChildren { get; set; }
    public double Price { get; set; }
}