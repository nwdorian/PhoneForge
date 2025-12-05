using System.Net.Http.Json;
using IntegrationTests.Core.Contracts;

namespace IntegrationTests.Core.Extensions;

internal static class HttpResponseMessageExtensions
{
    internal static async Task<CustomProblemDetails> GetProblemDetails(
        this HttpResponseMessage response
    )
    {
        if (response.IsSuccessStatusCode)
        {
            throw new InvalidOperationException("Successful response.");
        }

        CustomProblemDetails? problemDetails =
            await response.Content.ReadFromJsonAsync<CustomProblemDetails>();

        if (problemDetails is null)
        {
            throw new InvalidOperationException("Null problem details.");
        }

        return problemDetails;
    }
}
