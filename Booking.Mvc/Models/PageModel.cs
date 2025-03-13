namespace Booking.Mvc.Models;

public class PageModel<T>
{
    public List<T> Data { get; set; }
    public bool HasMore { get; set; }
    public bool HasLess { get; set; }
    public int TotalCount { get; set; }
    public int NextPage { get; set; }
    public int PrevPage { get; set; }
    public int CurrentPage { get; set; }
    public int CurrentPageSize { get; set; }
}