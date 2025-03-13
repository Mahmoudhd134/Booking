using Booking.Application.Dtos.Hotel;
using Booking.Application.Implementation;
using Booking.Application.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Booking.Application;

public static class Extensions
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddTransient<IHotelServices, HotelServices>();
        services.AddTransient<IBookServices, BookServices>();
        services.AddTransient<ICustomerServices, CustomerServices>();
        return services;
    }
}