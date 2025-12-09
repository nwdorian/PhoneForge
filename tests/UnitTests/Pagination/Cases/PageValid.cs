namespace UnitTests.Pagination.Cases;

public class PageValid : TheoryData<int>
{
    public PageValid()
    {
        Add(Page.FirstPage);
        Add(Page.FirstPage + 1);
    }
}
