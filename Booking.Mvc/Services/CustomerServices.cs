using System.Text.Json;
using Booking.Mvc.Models.Booking;

namespace Booking.Mvc.Services;

public class CustomerServices(HttpClient httpClient)
{
    public async Task<List<CustomerModel>> GetAll()
    {
        var response = await httpClient.GetAsync($"Customer");
        response.EnsureSuccessStatusCode();

        var json = await response.Content.ReadAsStringAsync();
        var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };

        var bookingDetail = JsonSerializer.Deserialize<List<CustomerModel>>(json, options);
        return bookingDetail;
    }

    public async Task AddCustomerAsync(CustomerModel customer)
    {
        var response = await httpClient.PostAsJsonAsync("Customer", customer);
        response.EnsureSuccessStatusCode();
    }
}