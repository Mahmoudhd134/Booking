using System.ComponentModel.DataAnnotations.Schema;
using Booking.Domain.Dtos;
using Booking.Domain.ErrorHandling;
using Booking.Domain.ErrorHandling.Errors;

namespace Booking.Domain.Models;

public class Book
{
    public int Id { get; set; }

    public int HotelId { get; set; }
    public Hotel Hotel { get; set; }

    public string CustomerId { get; set; }
    public Customer Customer { get; set; }

    public DateTime BookDate { get; set; }
    public DateTime CheckInDate { get; set; }
    public DateTime CheckOutDate { get; set; }
    public double Discount { get; set; }
    public double PriceBeforeDiscount { get; set; }
    public double PriceAfterDiscount { get; set; }

    public List<BookItem> Items { get; set; }

    public static Result<Book> Create(Hotel hotel, Customer customer, List<AddBookItemDto> items, DateTime checkInDate,
        DateTime checkOutDate, bool hasPreviousBooks)
    {
        var r = hotel.Rooms.Select(x => x.Id).ToHashSet();
        if (items.Any(i => r.Contains(i.Room.Id)) == false)
        {
            return RoomErrors.RoomUnAvailable;
        }

        var book = new Book()
        {
            Customer = customer,
            Hotel = hotel,
            CheckInDate = checkInDate,
            CheckOutDate = checkOutDate,
            BookDate = DateTime.UtcNow,
            Discount = hasPreviousBooks ? .05 : 0
        };
        var itemsResults = items.Select(item => BookItem.Create(item.Room, book, item.Adults, item.Children)).ToList();
        if (itemsResults.Any(x => x.IsFailure))
            return itemsResults.First(x => x.IsFailure).Error;
        book.Items = itemsResults.Select(x => x.Data).ToList();

        var numberOfDays = (checkOutDate - checkInDate).TotalDays;
        book.PriceBeforeDiscount = book.Items.Select(x => x.Price * Math.Ceiling(numberOfDays)).Sum();
        book.PriceAfterDiscount = book.PriceBeforeDiscount * (1 - book.Discount);

        return book;
    }
}