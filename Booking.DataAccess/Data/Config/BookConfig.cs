using Booking.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Booking.DataAccess.Data.Config;

public class BookConfig:IEntityTypeConfiguration<Book>
{
    public void Configure(EntityTypeBuilder<Book> builder)
    {
        builder.HasKey(x => x.Id);

        builder.HasOne(x => x.Hotel).WithMany(x => x.Books);
        builder.HasOne(x => x.Customer).WithMany(x => x.Books);
    }
}