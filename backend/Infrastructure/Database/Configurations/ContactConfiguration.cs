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

        builder.OwnsOne(
            contact => contact.FirstName,
            firstNameBuilder =>
            {
                firstNameBuilder.WithOwner();

                firstNameBuilder
                    .Property(firstName => firstName.Value)
                    .HasColumnName(nameof(Contact.FirstName))
                    .HasMaxLength(FirstName.MaxLength)
                    .IsRequired();
            }
        );

        builder.OwnsOne(
            contact => contact.LastName,
            lastNameBuilder =>
            {
                lastNameBuilder.WithOwner();

                lastNameBuilder
                    .Property(lastName => lastName.Value)
                    .HasColumnName(nameof(Contact.LastName))
                    .HasMaxLength(LastName.MaxLength)
                    .IsRequired();
            }
        );

        builder.OwnsOne(
            contact => contact.Email,
            emailBuilder =>
            {
                emailBuilder.WithOwner();

                emailBuilder
                    .Property(email => email.Value)
                    .HasColumnName(nameof(Contact.Email))
                    .HasMaxLength(Email.MaxLength)
                    .IsRequired();
            }
        );

        builder.OwnsOne(
            contact => contact.PhoneNumber,
            phoneNumberBuilder =>
            {
                phoneNumberBuilder.WithOwner();

                phoneNumberBuilder
                    .Property(phoneNumber => phoneNumber.Value)
                    .HasColumnName(nameof(Contact.PhoneNumber))
                    .HasMaxLength(PhoneNumber.MaxLength)
                    .IsRequired();
            }
        );

        builder.Property(contact => contact.DeletedOnUtc);

        builder.Property(contact => contact.Deleted).HasDefaultValue(false);

        builder.Property(contact => contact.CreatedOnUtc).IsRequired();

        builder.Property(contact => contact.ModifiedOnUtc);

        builder.HasQueryFilter(contact => !contact.Deleted);

        builder.Ignore(contact => contact.FullName);
    }
}
