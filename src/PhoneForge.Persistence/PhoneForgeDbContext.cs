using Microsoft.EntityFrameworkCore;
using PhoneForge.Domain.Contacts;
using PhoneForge.UseCases.Abstractions.Data;

namespace PhoneForge.Persistence;

public sealed class PhoneForgeDbContext : DbContext, IDbContext
{
    public PhoneForgeDbContext(DbContextOptions<PhoneForgeDbContext> options)
        : base(options) { }

    public DbSet<Contact> Contacts { get; set; }
}
