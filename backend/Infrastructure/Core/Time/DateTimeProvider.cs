using Domain.Core.Abstractions;

namespace Infrastructure.Core.Time;

/// <summary>
/// Represents the machine date time service.
/// </summary>
internal sealed class DateTimeProvider : IDateTimeProvider
{
    /// <inheritdoc />
    public DateTime UtcNow => DateTime.UtcNow;
}
