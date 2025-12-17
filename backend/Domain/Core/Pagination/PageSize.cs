using Domain.Core.Primitives;

namespace Domain.Core.Pagination;

/// <summary>
/// Creates a new <see cref="PageSize"/> instance based on the specified value.
/// </summary>
public sealed record PageSize
{
    /// <summary>
    /// Maximum page size.
    /// </summary>
    public const int MaximumPageSize = 30;

    /// <summary>
    /// Minimum page size,
    /// </summary>
    public const int MinimumPageSize = 10;

    private PageSize(int value)
    {
        Value = value;
    }

    /// <summary>
    /// Gets the page size value.
    /// </summary>
    public int Value { get; }

    /// <summary>
    /// Implicitly converts a <see cref="PageSize"/> instance to it's underlying integer value.
    /// </summary>
    /// <param name="pageSize"></param>
    public static implicit operator int(PageSize pageSize)
    {
        return pageSize.Value;
    }

    /// <summary>
    /// Creates a new page size instance based on the specified value.
    /// </summary>
    /// <param name="pageSize">The page size.</param>
    /// <returns>The result of the page size creation process containing the page size or an error.</returns>
    public static Result<PageSize> Create(int pageSize)
    {
        if (pageSize < MinimumPageSize)
        {
            return PaginationErrors.LowerThanAllowed;
        }

        if (pageSize > MaximumPageSize)
        {
            return PaginationErrors.GreaterThanAllowed;
        }

        return new PageSize(pageSize);
    }
}
