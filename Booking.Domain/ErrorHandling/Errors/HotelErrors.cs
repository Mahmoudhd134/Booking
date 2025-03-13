namespace Booking.Domain.ErrorHandling.Errors;

public class HotelErrors : Error
{
    private HotelErrors(string error) : base(error)
    {
    }

    public static readonly Error NotFound = new HotelErrors("Can not find this hotel");
}