namespace Booking.Mvc.Models.Booking;

public class BookingDetailModel
{
    public int Id { get; set; }
    public HotelModel Hotel { get; set; }
    public CustomerModel Customer { get; set; }
    public DateTime BookDate { get; set; }
    public DateTime CheckInDate { get; set; }
    public DateTime CheckOutDate { get; set; }
    public double Discount { get; set; }
    public double PriceBeforeDiscount { get; set; }
    public double PriceAfterDiscount { get; set; }
    public List<BookingItemModel> Items { get; set; }
}