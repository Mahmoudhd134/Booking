using Booking.Domain.Models;

namespace Booking.Domain.Dtos;

public class AddBookItemDto
{
    public Room Room { get; set; }
    public int Adults { get; set; }
    public int Children { get; set; }
}