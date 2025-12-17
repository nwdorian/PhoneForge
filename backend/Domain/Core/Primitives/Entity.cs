namespace Domain.Core.Primitives;

/// <summary>
/// Represents the base class that all entities derive from.
/// </summary>
public abstract class Entity
{
    /// <summary>
    /// Initializes a new instance of the <see cref="Entity"/> class.
    /// </summary>
    /// <param name="id">The entity identifier.</param>
    /// <exception cref="ArgumentException"></exception>
    protected Entity(Guid id)
    {
        if (id == Guid.Empty)
        {
            throw new ArgumentException("The identifier is required.", nameof(id));
        }

        Id = id;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="Entity"/> class.
    /// </summary>
    /// <remarks>
    /// Required by EF Core.
    /// </remarks>
    protected Entity() { }

    /// <summary>
    /// Gets or sets the entity identifier.
    /// </summary>
    public Guid Id { get; private set; }
}
