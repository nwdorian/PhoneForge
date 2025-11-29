using Domain.Core.Primitives;

namespace Domain.Core.Pagination;

/// <summary>
/// Creates a new <see cref="Page"/> instance based on the specified value.
/// </summary>
public sealed record Page
{
    private Page(int value)
    {
        Value = value;
    }

    /// <summary>
    /// Gets the page value.
    /// </summary>
    public int Value { get; }

    /// <summary>
    /// Implicitly converts a <see cref="Page"/> to it's underlying integer value.
    /// </summary>
    /// <param name="page">The page value.</param>
    public static implicit operator int(Page page)
    {
        return page.Value;
    }

    /// <summary>
    /// Creates a new <see cref="Page"/> instance based on the specified value.
    /// </summary>
    /// <param name="page">The page value.</param>
    /// <returns>The result of the page creation process containing the page or an error.</returns>
    public static Result<Page> Create(int page)
    {
        if (page < 1)
        {
            return PaginationErrors.InvalidPage;
        }

        return new Page(page);
    }
}
