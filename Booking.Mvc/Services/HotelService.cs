using System.Text.Json;
using Booking.Mvc.Models.Booking;

namespace Booking.Mvc.Services;

public class HotelService(HttpClient httpClient)
{
    public async Task<List<HotelModel>> GetHotelsAsync()
    {
        var response = await httpClient.GetAsync("Hotel");
        response.EnsureSuccessStatusCode();

        var json = await response.Content.ReadAsStringAsync();
        var options = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        };

        var hotels = JsonSerializer.Deserialize<List<HotelModel>>(json, options);
        return hotels;
    }
    
    public async Task AddHotelAsync(HotelModel hotel)
    {
        var response = await httpClient.PostAsJsonAsync("Hotel", hotel);
        response.EnsureSuccessStatusCode();
    }

}