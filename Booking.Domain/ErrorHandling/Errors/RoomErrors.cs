using Booking.Domain.Models;

namespace Booking.Domain.ErrorHandling.Errors;

public class RoomErrors : Error
{
    private RoomErrors(string error) : base(error)
    {
    }

    public static readonly Error RoomsNotFound = new RoomErrors("Can not find some of the rooms");
    public static readonly Error RoomUnAvailable = new RoomErrors("You can not book this room in this period of time");
}