using Booking.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Booking.DataAccess.Data;

public class BookingDbContext(DbContextOptions options) : DbContext(options)
{
    public DbSet<Hotel> Hotels { get; set; }
    public DbSet<Book> Books { get; set; }
    public DbSet<BookItem> BookItems { get; set; }
    public DbSet<Customer> Customers { get; set; }
    public DbSet<Room> Rooms { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(BookingDbContext).Assembly);
    }
};