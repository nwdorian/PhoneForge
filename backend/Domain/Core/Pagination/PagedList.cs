namespace Domain.Core.Pagination;

/// <summary>
/// Generic paged list.
/// </summary>
/// <typeparam name="T">The type of list.</typeparam>
/// <param name="items">Items in the list.</param>
/// <param name="page">Current page.</param>
/// <param name="pageSize">Page size.</param>
/// <param name="totalCount">Total count of the items.</param>
public sealed class PagedList<T>(
    IEnumerable<T> items,
    Page page,
    PageSize pageSize,
    int totalCount
)
{
    /// <summary>
    /// Gets the current page.
    /// </summary>
    public int Page { get; } = page;

    /// <summary>
    /// Gets the page size.
    /// </summary>
    public int PageSize { get; } = pageSize;

    /// <summary>
    /// Gets the total number of items.
    /// </summary>
    public int TotalCount { get; } = totalCount;

    /// <summary>
    /// Gets the total number of pages.
    /// </summary>
    public int TotalPages => (int)Math.Ceiling(TotalCount / (double)PageSize);

    /// <summary>
    /// Gets the flag indicating whether the next page exists.
    /// </summary>
    public bool HasNextPage => Page * PageSize < TotalCount;

    /// <summary>
    /// Gets the flag indicating whether the previous page exists.
    /// </summary>
    public bool HasPreviousPage => Page > 1;

    /// <summary>
    /// Gets the items.
    /// </summary>
    public IReadOnlyCollection<T> Items { get; } = items.ToList();
}
