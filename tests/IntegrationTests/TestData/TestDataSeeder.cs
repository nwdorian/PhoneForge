using Bogus;
using Domain.Contacts;
using Infrastructure.Database;

namespace IntegrationTests.TestData;

public class TestDataSeeder(PhoneForgeDbContext context)
{
    private readonly PhoneForgeDbContext _context = context;
    private readonly List<Contact> _contacts = [];

    public async Task SeedAsync()
    {
        await SeedContacts();

        await _context.SaveChangesAsync();
    }

    public async Task SeedContacts()
    {
        Faker<Contact> contactFaker = new Faker<Contact>().CustomInstantiator(f =>
            Contact.Create(
                FirstName.Create(f.Name.FirstName()).Value,
                LastName.Create(f.Name.LastName()).Value,
                Email.Create(f.Internet.Email()).Value,
                PhoneNumber.Create($"09{f.Random.Int(100000, 9999999)}").Value
            )
        );

        _contacts.AddRange(contactFaker.Generate(20));
        await _context.Contacts.AddRangeAsync(_contacts);
    }

    public Contact GetTestContact(int index = 0) => _contacts[index];
}
