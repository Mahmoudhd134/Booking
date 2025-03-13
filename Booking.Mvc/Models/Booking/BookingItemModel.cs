namespace Booking.Mvc.Models.Booking;

public class BookingItemModel
{
    public RoomDto Room { get; set; }
    public int NumberOfAdults { get; set; }
    public int NumberOfChildren { get; set; }
    public double Price { get; set; }
}