using System.ComponentModel.DataAnnotations.Schema;
using Booking.Domain.ErrorHandling;
using Booking.Domain.ErrorHandling.Errors;

namespace Booking.Domain.Models;

public class BookItem
{
    public int BookId { get; private set; }
    public Book Book { get; private set; }

    public int RoomId { get; private set; }
    public Room Room { get; private set; }

    public int NumberOfAdults { get; private set; }
    public int NumberOfChildren { get; private set; }
    public double Price { get; private set; }

    private BookItem()
    {
    }

    public static Result<BookItem> Create(Room room, Book book, int numberOfAdults, int numberOfChildren)
    {
        if (room.Adults < numberOfAdults)
        {
            return BookItemErrors.NumberOfAdultNotValid;
        }

        if (room.Children < numberOfChildren)
        {
            return BookItemErrors.NumberOfChildrenNotValid;
        }

        var item = new BookItem()
        {
            Room = room,
            Book = book,
            Price = room.PricePerNight,
            NumberOfAdults = numberOfAdults,
            NumberOfChildren = numberOfChildren
        };
        return item;
    }
}