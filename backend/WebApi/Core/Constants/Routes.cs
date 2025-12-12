namespace WebApi.Core.Constants;

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
        /// <summary>
        /// The base route for contact related endpoints.
        /// </summary>
        private const string Base = "contacts";

        /// <summary>
        /// The route used for retrieving a list of contacts.
        /// </summary>
        public const string Get = Base;

        /// <summary>
        /// The route used for retrieving contact by an identifier.
        /// </summary>
        public const string GetById = $"{Base}/{{contactId:guid}}";

        /// <summary>
        /// The route used for creating a new contact.
        /// </summary>
        public const string Create = Base;

        /// <summary>
        /// The route used for deleting a contact.
        /// </summary>
        public const string Delete = $"{Base}/{{contactId:guid}}";

        /// <summary>
        /// The route used for updating a contact.
        /// </summary>
        public const string Update = $"{Base}/{{contactId:guid}}";

        /// <summary>
        /// The route used to generate contacts report.
        /// </summary>
        public const string GenerateReport = $"{Base}/report";
    }
}
