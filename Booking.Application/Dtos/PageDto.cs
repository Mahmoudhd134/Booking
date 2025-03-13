using Microsoft.EntityFrameworkCore;

namespace Booking.Application.Dtos;

public class PageDto<T>
{
    public List<T> Data { get; private set; }
    public bool HasMore { get; private set; }
    public bool HasLess { get; private set; }
    public long TotalCount { get; private set; }
    public int NextPage { get; private set; }
    public int PrevPage { get; private set; }
    public int CurrentPage { get; private set; }
    public int CurrentPageSize { get; private set; }

    private PageDto()
    {
    }

    public static async Task<PageDto<T>> Create(IQueryable<T> query, int page = 1, int pageSize = 20,
        CancellationToken cancellationToken = default)
    {
        var data = await query
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync(cancellationToken);

        var totalCount = await query.CountAsync(cancellationToken);
        var lastPage = (int)Math.Ceiling(1D * totalCount / pageSize);
        var hasMore = data.Count == pageSize || page < lastPage;
        var hasLess = page > 1;

        return new PageDto<T>()
        {
            Data = data,
            TotalCount = totalCount,
            HasLess = hasLess,
            HasMore = hasMore,
            NextPage = hasMore ? page + 1 : 0,
            PrevPage = hasLess ? page - 1 : 0,
            CurrentPage = page,
            CurrentPageSize = pageSize
        };
    }
}