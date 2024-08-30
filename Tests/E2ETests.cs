using System.Threading.Tasks;
using Microsoft.Playwright;
using Xunit;

public class E2ETests
{
    [Fact]
    public async Task ProductPage_LoadsSuccessfully()
    {
        using var playwright = await Playwright.CreateAsync();
        await using var browser = await playwright.Chromium.LaunchAsync(new BrowserTypeLaunchOptions { Headless = true });
        var page = await browser.NewPageAsync();

        await page.GotoAsync("http://localhost:5000/api/products");

        var content = await page.ContentAsync();
        Assert.Contains("Product", content);
    }

    // Add more E2E tests
}
