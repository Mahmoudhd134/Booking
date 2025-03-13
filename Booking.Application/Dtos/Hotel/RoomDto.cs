using Booking.Domain.Models;

namespace Booking.Application.Dtos.Hotel;

public class RoomDto
{
    public int Id { get; set; }
    public RoomType RoomType { get; set; }
    public int Adults { get; set; }
    public int Children { get; set; }
    public double PricePerNight { get; set; }
}