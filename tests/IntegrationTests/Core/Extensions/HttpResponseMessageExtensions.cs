using System.Net;
using System.Net.Http.Json;
using Domain.Core.Primitives;
using IntegrationTests.Core.Contracts;

namespace IntegrationTests.Core.Extensions;

internal static class HttpResponseMessageExtensions
{
    internal static async Task AssertResponseErrorDetails(
        this HttpResponseMessage response,
        HttpStatusCode statusCode,
        Error expected
    )
    {
        if (response.IsSuccessStatusCode)
        {
            throw new InvalidOperationException("Successful response.");
        }

        CustomProblemDetails? problemDetails =
            await response.Content.ReadFromJsonAsync<CustomProblemDetails>()
            ?? throw new InvalidOperationException("Null problem details.");

        Assert.Equal(statusCode, response.StatusCode);
        Assert.NotNull(problemDetails);
        Assert.Equal(expected.Description, problemDetails.Errors[0]);
        Assert.Equal(expected.Code, problemDetails.Title);
    }
}
