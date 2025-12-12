using System.Globalization;
using Application.Contacts;
using Application.Core.Abstractions.Reports;
using Domain.Core.Abstractions;
using HandlebarsDotNet;
using Infrastructure.Documents;

namespace Infrastructure.Reports;

/// <summary>
/// Represents the reports service.
/// </summary>
public class ReportsService(IDateTimeProvider dateTimeProvider) : IReportsService
{
    /// <inheritdoc />
    public async Task GenerateContactsReport(List<ContactResponse> contacts)
    {
        string templatePath = "../Infrastructure/Templates/ContactsReportTemplate.hbs";
        string templateContent = await File.ReadAllTextAsync(templatePath);

        HandlebarsTemplate<object, object> template = Handlebars.Compile(templateContent);

        Handlebars.RegisterHelper(
            "formatDateTime",
            (context, arguments) =>
            {
                if (arguments[0] is DateTime dateTime)
                {
                    return dateTime.ToString(
                        "d-MMM-yyyy HH:mm",
                        CultureInfo.InvariantCulture
                    );
                }
                return arguments[0]?.ToString() == "";
            }
        );

        var data = new { contacts };

        string html = template(data);

        string path = "../../documents";

        string name =
            $"contacts-report-{dateTimeProvider.UtcNow.ToString("yyMMddHHmm", CultureInfo.InvariantCulture)}.pdf";

        await PdfService.GeneratePdfReport(html, path, name);
    }
}
