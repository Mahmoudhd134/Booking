namespace Booking.Application.Dtos.Book;

public class BookPrice
{
    public double Discount { get; set; }
    public double PriceBeforeDiscount { get; set; }
    public double PriceAfterDiscount { get; set; }
}