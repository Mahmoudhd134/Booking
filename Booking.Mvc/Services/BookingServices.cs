using System.Text.Json;
using Booking.Mvc.Models;
using Booking.Mvc.Models.Booking;

namespace Booking.Mvc.Services;

public class BookingService(HttpClient httpClient)
{
    public async Task<PageModel<BookForListModel>> GetBookingsAsync(int page, int pageSize)
    {
        var url = $"Booking?page={page}&pageSize={pageSize}";

        var response = await httpClient.GetAsync(url);
        if (response.IsSuccessStatusCode == false)
        {
            throw new Exception($"Error retrieving bookings. Status code: {response.StatusCode}");
        }

        var json = await response.Content.ReadAsStringAsync();
        var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
        var bookingResponse = JsonSerializer.Deserialize<PageModel<BookForListModel>>(json, options);
        return bookingResponse;
    }

    public async Task<BookingDetailModel> GetBookingByIdAsync(int id)
    {
        var response = await httpClient.GetAsync($"Booking/{id}");
        response.EnsureSuccessStatusCode();

        var json = await response.Content.ReadAsStringAsync();
        var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };

        var bookingDetail = JsonSerializer.Deserialize<BookingDetailModel>(json, options);
        return bookingDetail;
    }
}