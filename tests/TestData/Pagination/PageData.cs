using Domain.Core.Pagination;

namespace TestData.Pagination;

public static class PageData
{
    public static readonly Page ValidPage = Page.Create(1).Value;

    public static readonly int InvalidPage = -1;
}
