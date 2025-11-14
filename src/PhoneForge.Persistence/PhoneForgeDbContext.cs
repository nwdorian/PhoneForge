using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using PhoneForge.Domain.Contacts;
using PhoneForge.UseCases.Abstractions.Data;
using SharedKernel;

namespace PhoneForge.Persistence;

public sealed class PhoneForgeDbContext : DbContext, IDbContext
{
    private readonly IDateTimeProvider _dateTimeProvider;

    public PhoneForgeDbContext(DbContextOptions<PhoneForgeDbContext> options, IDateTimeProvider dateTimeProvider)
        : base(options)
    {
        _dateTimeProvider = dateTimeProvider;
    }

    public DbSet<Contact> Contacts { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(PhoneForgeDbContext).Assembly);
    }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        var utcNow = _dateTimeProvider.UtcNow;

        UpdateAuditableEntities(utcNow);

        UpdateSoftDeletableEntities(utcNow);

        return await base.SaveChangesAsync(cancellationToken);
    }

    private void UpdateAuditableEntities(DateTime utcNow)
    {
        var entities = ChangeTracker.Entries<IAuditableEntity>().ToList();

        foreach (var entity in entities)
        {
            if (entity.State == EntityState.Added)
            {
                entity.Property(nameof(IAuditableEntity.CreatedOnUtc)).CurrentValue = utcNow;
            }

            if (entity.State == EntityState.Modified)
            {
                entity.Property(nameof(IAuditableEntity.ModifiedOnUtc)).CurrentValue = utcNow;
            }
        }
    }

    private void UpdateSoftDeletableEntities(DateTime utcNow)
    {
        var entities = ChangeTracker
            .Entries<ISoftDeletableEntity>()
            .Where(e => e.State == EntityState.Deleted)
            .ToList();

        foreach (var entity in entities)
        {
            entity.Property(nameof(ISoftDeletableEntity.DeletedOnUtc)).CurrentValue = utcNow;

            entity.Property(nameof(ISoftDeletableEntity.Deleted)).CurrentValue = true;

            entity.State = EntityState.Modified;

            UpdateDeletedEntityReferencesToUnchanged(entity);
        }
    }

    [System.Diagnostics.CodeAnalysis.SuppressMessage(
        "Minor Code Smell",
        "S3267:Loops should be simplified with \"LINQ\" expressions",
        Justification = "Explicit foreach with a Where filter is clearer for recursive EF Core change-tracker mutation. Using a LINQ Select projection would reduce readability and make recursion harder to follow."
    )]
    private static void UpdateDeletedEntityReferencesToUnchanged(EntityEntry entity)
    {
        if (!entity.References.Any())
        {
            return;
        }

        foreach (var reference in entity.References.Where(r => r.TargetEntry!.State == EntityState.Deleted))
        {
            reference.TargetEntry!.State = EntityState.Unchanged;

            UpdateDeletedEntityReferencesToUnchanged(reference.TargetEntry);
        }
    }
}
