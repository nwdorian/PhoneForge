namespace IntegrationTests.Core.Contracts;

internal sealed class CustomProblemDetails
{
    public string? Type { get; set; }
    public string? Title { get; set; }
    public int Status { get; set; }
    public string? Detail { get; set; }
    public List<string> Errors { get; set; } = [];
}
