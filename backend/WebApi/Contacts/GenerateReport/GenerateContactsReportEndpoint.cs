using Application.Contacts.GenerateReport;
using Application.Core.Abstractions.Messaging;
using Domain.Core.Primitives;
using WebApi.Core;
using WebApi.Core.Constants;
using WebApi.Core.Extensions;
using WebApi.Core.Infrastructure;

namespace WebApi.Contacts.GenerateReport;

internal sealed class GenerateContactsReportEndpoint : IEndpoint
{
    public const string Name = "GenerateContactsReport";

    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet(Routes.Contacts.GenerateReport, Handler)
            .WithNameAndTags(Name, Tags.Contacts)
            .Produces(StatusCodes.Status204NoContent)
            .MapToApiVersion(1);
    }

    private static async Task<IResult> Handler(
        [AsParameters] GenerateContactsReportRequest request,
        ICommandHandler<GenerateContactsReportCommand> useCase,
        CancellationToken cancellationToken
    )
    {
        GenerateContactsReportCommand command = new(
            request.SearchTerm,
            request.SortColumn,
            request.SortOrder
        );

        Result result = await useCase.Handle(command, cancellationToken);

        return result.Match(Results.NoContent, CustomResults.Problem);
    }
}
