using Domain.Contacts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Database.Configurations;

/// <summary>
/// Represents the configuration for the <see cref="Contact"/> entity.
/// </summary>
public class ContactConfiguration : IEntityTypeConfiguration<Contact>
{
    /// <summary>
    /// Configures the <see cref="Contact"/> entity.
    /// </summary>
    /// <param name="builder">The builder used to configure the <see cref="Contact"/> entity.</param>
    public void Configure(EntityTypeBuilder<Contact> builder)
    {
        builder.HasKey(contact => contact.Id);

        builder.ComplexProperty(
            c => c.FirstName,
            firstName =>
                firstName
                    .Property(f => f.Value)
                    .HasColumnName(nameof(Contact.FirstName))
                    .HasMaxLength(FirstName.MaxLength)
                    .IsRequired()
        );

        builder.ComplexProperty(
            c => c.LastName,
            lastName =>
                lastName
                    .Property(l => l.Value)
                    .HasColumnName(nameof(Contact.LastName))
                    .HasMaxLength(LastName.MaxLength)
                    .IsRequired()
        );

        builder.ComplexProperty(
            c => c.Email,
            email =>
                email
                    .Property(e => e.Value)
                    .HasColumnName(nameof(Contact.Email))
                    .HasMaxLength(Email.MaxLength)
                    .IsRequired()
        );

        builder.ComplexProperty(
            c => c.PhoneNumber,
            phoneNumber =>
                phoneNumber
                    .Property(p => p.Value)
                    .HasColumnName(nameof(Contact.PhoneNumber))
                    .HasMaxLength(PhoneNumber.MaxLength)
                    .IsRequired()
        );

        builder.Property(contact => contact.DeletedOnUtc);

        builder.Property(contact => contact.Deleted).HasDefaultValue(false);

        builder.Property(contact => contact.CreatedOnUtc).IsRequired();

        builder.Property(contact => contact.ModifiedOnUtc);

        builder.HasQueryFilter(contact => !contact.Deleted);

        builder.Ignore(contact => contact.FullName);
    }
}
