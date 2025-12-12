using PuppeteerSharp;
using PuppeteerSharp.Media;

namespace Infrastructure.Documents;

/// <summary>
/// Represents the PDF service.
/// </summary>
public static class PdfService
{
    /// <inheritdoc />
    public static async Task GeneratePdfReport(string html, string path, string name)
    {
        BrowserFetcher browserFetcher = new();
        await browserFetcher.DownloadAsync();

        using IBrowser browser = await Puppeteer.LaunchAsync(
            new LaunchOptions { Headless = true }
        );

        using IPage page = await browser.NewPageAsync();

        await page.SetContentAsync(html);

        string outputPath = Path.Combine(path, name);

        await page.PdfAsync(
            outputPath,
            new PdfOptions
            {
                Format = PaperFormat.A4,
                MarginOptions = new MarginOptions
                {
                    Top = "60px",
                    Right = "20px",
                    Bottom = "60px",
                    Left = "20px",
                },
                PrintBackground = true,
                DisplayHeaderFooter = true,
                HeaderTemplate = """
                <div style='font-size: 14px; text-align: center; padding: 10px;'>
                    <span style='margin-left: 20px;'>Generated on <span class='date'></span></span>
                </div>
                """,
                FooterTemplate = """
                <div style='width: 100%; font-size: 14px; text-align: center; padding: 10px;'>
                    <span>Page <span class='pageNumber'></span> of <span class='totalPages'></span></span>
                </div>
                """,
            }
        );
    }
}
