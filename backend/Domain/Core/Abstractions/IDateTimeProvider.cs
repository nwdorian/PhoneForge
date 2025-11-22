namespace Domain.Core.Abstractions;

/// <summary>
/// Represents the interface for getting the current date and time.
/// </summary>
public interface IDateTimeProvider
{
    /// <summary>
    /// Gets the current date and time in UTC format.
    /// </summary>
    DateTime UtcNow { get; }
}
