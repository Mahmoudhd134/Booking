using Booking.Application.Dtos.Book;
using Booking.Application.Dtos.Hotel;
using Booking.Application.Services;
using Booking.DataAccess.Data;
using Booking.Domain.ErrorHandling;
using Booking.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Booking.Application.Implementation;

public class HotelServices(BookingDbContext context) : IHotelServices
{
    public async Task<Result<int>> Add(AddHotelDto model, CancellationToken cancellationToken)
    {
        var hotelResult = Hotel.Create(model.BrachName);
        if (hotelResult.IsFailure)
            return hotelResult.Error;

        var hotel = hotelResult.Data;
        var rooms = model.AddRoomDtos.Select(x => Room.Create(hotel, x.RoomType, x.Adults, x.Children, x.PricePerNight))
            .ToList();
        hotel.AddRooms(rooms);

        context.Hotels.Add(hotel);
        await context.SaveChangesAsync(cancellationToken);
        return hotel.Id;
    }

    public async Task<Result<List<HotelDto>>> All(CancellationToken cancellationToken = default)
    {
        return await context.Hotels
            .Select(x => new HotelDto()
            {
                Id = x.Id,
                BranchName = x.BranchName,
            }).ToListAsync(cancellationToken);
    }

    public async Task<Result<List<RoomDto>>> GetAvailableRoomsForHotelInPeriod(int hotelId, DateTime checkIn,
        DateTime checkOut,
        CancellationToken cancellationToken = default)
    {
        var availableRooms = await context.Rooms
            .Where(x => x.HotelId == hotelId)
            .Where(x => x.BookItems
                .Any(bi => bi.Book.CheckInDate < checkOut && bi.Book.CheckOutDate > checkIn) == false
            )
            .Select(x => new RoomDto
            {
                Id = x.Id,
                Children = x.Children,
                RoomType = x.RoomType,
                PricePerNight = x.PricePerNight,
                Adults = x.Adults,
            })
            .ToListAsync(cancellationToken);
        return availableRooms;
    }
}