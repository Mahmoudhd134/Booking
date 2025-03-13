using Booking.DataAccess.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Booking.DataAccess;

public static class Extensions
{
    public static IServiceCollection AddDataAccess(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<BookingDbContext>(o =>
            o.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));
        return services;
    }
}