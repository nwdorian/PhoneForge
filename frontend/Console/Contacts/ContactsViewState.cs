namespace Console.Contacts;

internal sealed class ContactsViewState
{
    public string SearchTerm { get; set; } = string.Empty;
    public int Page { get; set; } = 1;
    public int PageSize { get; set; } = 10;
    public string SortColumn { get; set; } = "first_name";
    public string SortOrder { get; set; } = "asc";
}
