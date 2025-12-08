using Domain.Core.Pagination;

namespace WebApi.Core.Constants;

/// <summary>
/// Default pagination values.
/// </summary>
public static class Pagination
{
    /// <summary>
    /// Default page.
    /// </summary>
    public const int DefaultPage = Page.FirstPage;

    /// <summary>
    /// Default page size.
    /// </summary>
    public const int DefaultPageSize = PageSize.MinimumPageSize;

    /// <summary>
    /// Default sort order.
    /// </summary>
    public const string DefaultSortOrder = "asc";

    /// <summary>
    /// Default sort column.
    /// </summary>
    public const string DefaultSortColum = "created_on";
}
