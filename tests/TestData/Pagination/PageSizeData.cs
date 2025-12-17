namespace TestData.Pagination;

public static class PageSizeData
{
    public static readonly PageSize MinimumPageSize = PageSize
        .Create(PageSize.MinimumPageSize)
        .Value;

    public static readonly int GreaterThenAllowedPageSize = PageSize.MaximumPageSize + 1;
    public static readonly int LowerThenAllowedPageSize = PageSize.MinimumPageSize - 1;
}
