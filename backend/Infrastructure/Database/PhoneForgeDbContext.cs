using Application.Core.Abstractions.Data;
using Domain.Contacts;
using Domain.Core.Abstractions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Infrastructure.Database;

/// <summary>
/// Represents the applications database context.
/// </summary>
public sealed class PhoneForgeDbContext : DbContext, IDbContext
{
    private readonly IDateTimeProvider _dateTimeProvider;

    /// <summary>
    /// Initializes a new instance of the <see cref="PhoneForgeDbContext"/> class.
    /// </summary>
    /// <param name="options">The database context options.</param>
    /// <param name="dateTimeProvider">The current date and time in UTC format.</param>
    public PhoneForgeDbContext(
        DbContextOptions<PhoneForgeDbContext> options,
        IDateTimeProvider dateTimeProvider
    )
        : base(options)
    {
        _dateTimeProvider = dateTimeProvider;
    }

    /// <inheritdoc />
    public DbSet<Contact> Contacts { get; set; }

    /// <inheritdoc />
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(
            typeof(PhoneForgeDbContext).Assembly
        );
    }

    /// <inheritdoc />
    public override async Task<int> SaveChangesAsync(
        CancellationToken cancellationToken = default
    )
    {
        var utcNow = _dateTimeProvider.UtcNow;

        UpdateAuditableEntities(utcNow);

        UpdateSoftDeletableEntities(utcNow);

        return await base.SaveChangesAsync(cancellationToken);
    }

    /// <summary>
    /// Updates the entities implementing the <see cref="IAuditableEntity"/> interface.
    /// </summary>
    /// <param name="utcNow">The current date and time in UTC format.</param>
    private void UpdateAuditableEntities(DateTime utcNow)
    {
        var entities = ChangeTracker.Entries<IAuditableEntity>().ToList();

        foreach (var entity in entities)
        {
            if (entity.State == EntityState.Added)
            {
                entity.Property(nameof(IAuditableEntity.CreatedOnUtc)).CurrentValue =
                    utcNow;
            }

            if (entity.State == EntityState.Modified)
            {
                entity.Property(nameof(IAuditableEntity.ModifiedOnUtc)).CurrentValue =
                    utcNow;
            }
        }
    }

    /// <summary>
    /// Updates the entities implementing the <see cref="ISoftDeletableEntity"/> interface.
    /// </summary>
    /// <param name="utcNow">The current date and time in UTC format.</param>
    private void UpdateSoftDeletableEntities(DateTime utcNow)
    {
        var entities = ChangeTracker
            .Entries<ISoftDeletableEntity>()
            .Where(e => e.State == EntityState.Deleted)
            .ToList();

        foreach (var entity in entities)
        {
            entity.Property(nameof(ISoftDeletableEntity.DeletedOnUtc)).CurrentValue =
                utcNow;

            entity.Property(nameof(ISoftDeletableEntity.Deleted)).CurrentValue = true;

            entity.State = EntityState.Modified;

            UpdateDeletedEntityReferencesToUnchanged(entity);
        }
    }

    /// <summary>
    /// Updates the specified entity entry's referenced entries in the deleted state to the unchanged state.
    /// This method is recursive.
    /// </summary>
    /// <param name="entity">The entity entry.</param>
    private static void UpdateDeletedEntityReferencesToUnchanged(EntityEntry entity)
    {
        if (!entity.References.Any())
        {
            return;
        }

        var references = entity
            .References.Where(r => r.TargetEntry?.State == EntityState.Deleted)
            .Select(r => r.TargetEntry!);

        foreach (var reference in references)
        {
            reference.State = EntityState.Unchanged;

            UpdateDeletedEntityReferencesToUnchanged(reference);
        }
    }
}
