namespace Booking.Domain.ErrorHandling.Errors;

public class BookItemErrors : Error
{
    private BookItemErrors(string error) : base(error)
    {
    }

    public static readonly Error NumberOfAdultNotValid = new BookItemErrors("Invalid Number Of Adults");
    public static readonly Error NumberOfChildrenNotValid = new BookItemErrors("Invalid Number Of Children");
}