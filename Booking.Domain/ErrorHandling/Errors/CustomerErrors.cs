namespace Booking.Domain.ErrorHandling.Errors;

public class CustomerErrors : Error
{
    private CustomerErrors(string error) : base(error)
    {
    }

    public static readonly Error NotFound = new CustomerErrors("Can not find this customer");
    public static readonly Error NIdExists = new CustomerErrors("This national ID is already in use");
}