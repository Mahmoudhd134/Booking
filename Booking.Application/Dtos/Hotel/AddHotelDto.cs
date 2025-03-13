using Booking.Domain.Dtos;

namespace Booking.Application.Dtos.Hotel;

public class AddHotelDto
{
    public string BrachName { get; set; }
    public List<AddRoomDto> AddRoomDtos { get; set; }
}