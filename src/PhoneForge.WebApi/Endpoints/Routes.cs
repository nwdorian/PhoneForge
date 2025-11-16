namespace PhoneForge.WebApi.Endpoints;

/// <summary>
/// Provides route definitions for the application's endpoints.
/// </summary>
public static class Routes
{
    /// <summary>
    /// Contains route definitions related to contacts.
    /// </summary>
    public static class Contacts
    {
        private const string Base = "contacts";

        /// <summary>
        /// The route used for creating a new contact.
        /// </summary>
        public const string Create = Base;

        /// <summary>
        /// The route used for retrieving a contact by identifier.
        /// </summary>
        public const string GetById = $"{Base}/{{id:guid}}";
    }
}
