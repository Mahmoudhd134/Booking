using Booking.Domain.ErrorHandling;

namespace Booking.Domain.Models;

public class Customer
{
    public string NId { get; private set; }
    public string Name { get; private set; }
    public string PhoneNumber { get; private set; }
    public List<Book> Books { get; private set; }

    private Customer()
    {
    }

    public static Result<Customer> Create(string nId, string name, string phoneNumber) =>
        new Customer
        {
            NId = nId,
            PhoneNumber = phoneNumber,
            Name = name
        };
}