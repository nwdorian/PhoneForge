using System.Reflection;
using Application.Core.Abstractions.Messaging;
using Domain.Contacts;
using Infrastructure.Database;

namespace ArchitectureTests;

public abstract class BaseTest
{
    protected static readonly Assembly DomainAssembly = typeof(Contact).Assembly;
    protected static readonly Assembly ApplicationAssembly = typeof(ICommand).Assembly;
    protected static readonly Assembly InfrastructureAssembly =
        typeof(PhoneForgeDbContext).Assembly;
    protected static readonly Assembly WebApiAssembly = typeof(Program).Assembly;
}
