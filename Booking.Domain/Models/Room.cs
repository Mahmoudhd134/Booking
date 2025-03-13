using System.ComponentModel.DataAnnotations.Schema;

namespace Booking.Domain.Models;

public class Room
{
    public int Id { get; private set; }
    public int HotelId { get; private set; }
    public Hotel Hotel { get; private set; }
    public RoomType RoomType { get; private set; }
    public int Adults { get; private set; }
    public int Children { get; private set; }
    public double PricePerNight { get; private set; }
    public List<BookItem> BookItems { get; private set; }

    private Room()
    {
    }

    public static Room Create(Hotel hotel, RoomType roomType, int adults, int children, double pricePerNight)
    {
        return new Room()
        {
            Hotel = hotel,
            Adults = adults,
            Children = children,
            RoomType = roomType,
            PricePerNight = pricePerNight,
        };
    }
}