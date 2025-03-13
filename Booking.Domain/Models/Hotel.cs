using Booking.Domain.Dtos;
using Booking.Domain.ErrorHandling;

namespace Booking.Domain.Models;

public class Hotel
{
    public int Id { get; private set; }
    public string BranchName { get; private set; }
    public List<Room> Rooms { get; private set; } = [];
    public List<Book> Books { get; private set; } = [];

    private Hotel()
    {
    }

    public static Result<Hotel> Create(string branchName)
    {
        var hotel = new Hotel()
        {
            BranchName = branchName,
        };
        return hotel;
    }

    public Result AddRooms(List<Room> rooms)
    {
        Rooms.AddRange(rooms);
        return Result.Success();
    }
}