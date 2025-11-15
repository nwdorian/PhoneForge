using SharedKernel;

namespace PhoneForge.Infrastructure.Time;

/// <summary>
/// Represents the machine date time service.
/// </summary>
internal sealed class DateTimeProvider : IDateTimeProvider
{
    /// <inheritdoc />
    public DateTime UtcNow => DateTime.UtcNow;
}
