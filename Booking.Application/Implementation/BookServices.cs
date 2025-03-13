using Booking.Application.Dtos;
using Booking.Application.Dtos.Book;
using Booking.Application.Dtos.BookItem;
using Booking.Application.Dtos.Hotel;
using Booking.Application.Services;
using Booking.DataAccess.Data;
using Booking.Domain.ErrorHandling;
using Booking.Domain.ErrorHandling.Errors;
using Booking.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Booking.Application.Implementation;

public class BookServices(BookingDbContext context) : IBookServices
{
    public async Task<Result<int>> Add(string customerNId,
        int hotelId,
        List<AddBookItemDto> items,
        DateTime checkIn,
        DateTime checkOut,
        CancellationToken cancellationToken = default)
    {
        var customer = await context.Customers.FirstOrDefaultAsync(x => x.NId == customerNId, cancellationToken);
        if (customer is null)
        {
            return CustomerErrors.NotFound;
        }


        var roomIds = items.Select(x => x.RoomId).ToList();
        var rooms = await context.Rooms
            .Where(x => roomIds.Contains(x.Id))
            .ToDictionaryAsync(x => x.Id, x => x, cancellationToken);
        if (rooms.Count < roomIds.Count)
        {
            return RoomErrors.RoomsNotFound;
        }

        var hotel = await context.Hotels
            .Include(x => x.Rooms
                .Where(room => roomIds.Contains(room.Id))
                .Where(room => room.BookItems
                    .Any(bi => bi.Book.CheckInDate < checkOut && bi.Book.CheckOutDate > checkIn) == false
                )
            )
            .FirstOrDefaultAsync(x => x.Id == hotelId, cancellationToken);

        if (hotel is null)
        {
            return HotelErrors.NotFound;
        }


        var i = items.Select(x => new Domain.Dtos.AddBookItemDto
        {
            Adults = x.Adults,
            Children = x.Children,
            Room = rooms[x.RoomId]
        }).ToList();

        var hasPrevious = await context.Books.AnyAsync(x => x.CustomerId == customerNId && x.HotelId == hotelId,
            cancellationToken);


        var bookResult = Book.Create(hotel, customer, i, checkIn, checkOut, hasPrevious);

        if (bookResult.IsFailure)
            return bookResult.Error;

        var book = bookResult.Data;

        context.Books.Add(book);
        await context.SaveChangesAsync(cancellationToken);

        return book.Id;
    }

    public async Task<Result<BookPrice>> GetPrice(string customerId, int hotelId, List<AddBookItemDto> items,
        DateTime checkIn, DateTime checkOut,
        CancellationToken cancellationToken = default)
    {
        var customer = await context.Customers.FirstOrDefaultAsync(x => x.NId == customerId, cancellationToken);
        if (customer is null)
        {
            return CustomerErrors.NotFound;
        }


        var roomIds = items.Select(x => x.RoomId).ToList();
        var rooms = await context.Rooms
            .Where(x => roomIds.Contains(x.Id))
            .ToDictionaryAsync(x => x.Id, x => x, cancellationToken);
        if (rooms.Count < roomIds.Count)
        {
            return RoomErrors.RoomsNotFound;
        }

        var hotel = await context.Hotels
            .Include(x => x.Rooms
                .Where(room => roomIds.Contains(room.Id))
                .Where(room => room.BookItems
                    .Any(bi => bi.Book.CheckInDate < checkOut && bi.Book.CheckOutDate > checkIn) == false
                )
            )
            .FirstOrDefaultAsync(x => x.Id == hotelId, cancellationToken);

        if (hotel is null)
        {
            return HotelErrors.NotFound;
        }


        var i = items.Select(x => new Domain.Dtos.AddBookItemDto
        {
            Adults = x.Adults,
            Children = x.Children,
            Room = rooms[x.RoomId]
        }).ToList();

        var hasPrevious = await context.Books.AnyAsync(x => x.CustomerId == customerId && x.HotelId == hotelId,
            cancellationToken);


        var bookResult = Book.Create(hotel, customer, i, checkIn, checkOut, hasPrevious);
        if (bookResult.IsFailure)
        {
            return bookResult.Error;
        }

        return new BookPrice()
        {
            PriceAfterDiscount = bookResult.Data.PriceAfterDiscount,
            PriceBeforeDiscount = bookResult.Data.PriceBeforeDiscount,
            Discount = bookResult.Data.Discount,
        };
    }

    public async Task<Result<PageDto<BookForListDto>>> All(int page = 1, int pageSize = 10)
    {
        var query = context.Books
            .OrderByDescending(x => x.BookDate)
            .Select(x => new BookForListDto()
            {
                Id = x.Id,
                Price = x.PriceAfterDiscount,
                CustomerName = x.Customer.Name
            })
            .AsQueryable();

        return await PageDto<BookForListDto>.Create(query, page, pageSize);
    }

    public async Task<Result<BookDto>> GetById(int id, CancellationToken cancellationToken = default)
    {
        var data = await context.Books
            .Where(x => x.Id == id)
            .Select(x => new BookDto()
            {
                Id = id,
                CheckInDate = x.CheckInDate,
                CheckOutDate = x.CheckOutDate,
                Discount = x.Discount,
                BookDate = x.BookDate,
                PriceAfterDiscount = x.PriceAfterDiscount,
                PriceBeforeDiscount = x.PriceBeforeDiscount,
                Customer = new CustomerDto()
                {
                    NId = x.Customer.NId,
                    Name = x.Customer.Name,
                    PhoneNumber = x.Customer.PhoneNumber
                },
                Hotel = new HotelDto()
                {
                    Id = x.Hotel.Id,
                    BranchName = x.Hotel.BranchName,
                },
                Items = x.Items.Select(i => new BookItemDto()
                {
                    Price = i.Price,
                    NumberOfAdults = i.NumberOfAdults,
                    NumberOfChildren = i.NumberOfChildren,
                    Room = new RoomDto()
                    {
                        Id = i.Room.Id,
                        Children = i.Room.Children,
                        Adults = i.Room.Adults,
                        RoomType = i.Room.RoomType,
                        PricePerNight = i.Room.PricePerNight
                    }
                }).ToList()
            })
            .FirstOrDefaultAsync(cancellationToken);
        if (data is null)
        {
            return BookErrors.NotFound;
        }

        return data;
    }
}