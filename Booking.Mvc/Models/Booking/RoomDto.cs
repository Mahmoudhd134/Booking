namespace Booking.Mvc.Models.Booking;

public class RoomDto
{
    public int Id { get; set; }
    public RoomType RoomType { get; set; }
    public int Adults { get; set; }
    public int Children { get; set; }
    public double PricePerNight { get; set; }
}
public enum RoomType
{
    Single = 1,
    Double = 2,
    Suit = 3
}