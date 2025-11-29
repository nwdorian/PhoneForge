using Domain.Core.Primitives;

namespace Domain.Core.Pagination;

/// <summary>
/// Contains pagination errors.
/// </summary>
public static class PaginationErrors
{
    /// <summary>
    /// An error indicating that the page is invalid.
    /// </summary>
    public static Error InvalidPage =>
        Error.Validation("Page.InvalidPage", "Page must be greater then or equal to 1.");

    /// <summary>
    /// An error indicating that the page size is greater than allowed.
    /// </summary>
    public static Error GreaterThanAllowed =>
        Error.Validation(
            "PageSize.GreaterThanAllowed",
            $"Maximum page size is {PageSize.MaximumPageSize}"
        );

    /// <summary>
    /// An error indicating that the page size lower than allowed.
    /// </summary>
    public static Error LowerThanAllowed =>
        Error.Validation(
            "PageSize.LowerThanAllowed",
            $"Minimum page size is {PageSize.MinimumPageSize}"
        );
}
