using Domain.Core.Pagination;

namespace TestData.Pagination;

public static class PageData
{
    public static readonly Page FirstPage = Page.Create(1).Value;
    public static readonly Page SecondPage = Page.Create(2).Value;

    public static readonly int InvalidPage = -1;
}
