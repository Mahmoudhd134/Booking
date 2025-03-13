namespace Booking.Application.Dtos.Book;

public class BookDto
{
    public int Id { get; set; }

    public HotelDto Hotel { get; set; }

    public CustomerDto Customer { get; set; }

    public DateTime BookDate { get; set; }
    public DateTime CheckInDate { get; set; }
    public DateTime CheckOutDate { get; set; }
    public double Discount { get; set; }
    public double PriceBeforeDiscount { get; set; }
    public double PriceAfterDiscount { get; set; }

    public List<BookItemDto> Items { get; set; }
}