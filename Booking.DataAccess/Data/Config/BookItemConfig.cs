using Booking.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Booking.DataAccess.Data.Config;

public class BookItemConfig : IEntityTypeConfiguration<BookItem>
{
    public void Configure(EntityTypeBuilder<BookItem> builder)
    {
        builder.HasKey(x => new { x.BookId, x.RoomId });

        builder.HasOne(x => x.Book).WithMany(x => x.Items);
        builder.HasOne(x => x.Room).WithMany(x => x.BookItems).OnDelete(DeleteBehavior.NoAction);
    }
}