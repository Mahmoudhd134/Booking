using Booking.Domain.Models;

namespace Booking.Domain.Dtos;

public record AddRoomDto(
    RoomType RoomType,
    int Adults,
    int Children,
    double PricePerNight
);