namespace Booking.Domain.ErrorHandling.Errors;

public class BookErrors : Error
{
    private BookErrors(string error) : base(error)
    {
    }

    public static readonly Error NotFound = new BookErrors("This book does not exist");
}