namespace Booking.Mvc.Models.Booking;

public class CustomerModel
{
    // Note: The JSON property is "nId" so ensure the casing is handled via serializer options
    public string NId { get; set; }
    public string Name { get; set; }
    public string PhoneNumber { get; set; }
}