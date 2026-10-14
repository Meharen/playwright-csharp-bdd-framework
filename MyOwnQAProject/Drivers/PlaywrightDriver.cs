using Microsoft.Playwright;
using Enterprise.Automation.Framework.Pages;

namespace Enterprise.Automation.Framework.Drivers
{
    public class PlaywrightDriver
    {
        public IPlaywright? Playwright { get; private set; }
        public IBrowser? Browser { get; private set; }
        public IBrowserContext? BrowserContext { get; private set; }
        public IPage? Page { get; private set; }

        public async Task<IPage> InitializeAsync(
            string browserName = "chromium",
            bool headless = false,
            float timeout = 30000)
        {
            Playwright = await Microsoft.Playwright.Playwright.CreateAsync();

            var launchOptions = new BrowserTypeLaunchOptions
            {
                Headless = headless,
                Args = new[] { "--start-maximized" }
            };

            switch (browserName.ToLower())
            {
                case "chrome":
                    launchOptions.Channel = "chrome";
                    Browser = await Playwright.Chromium.LaunchAsync(launchOptions);
                    break;
                case "msedge":
                    launchOptions.Channel = "msedge";
                    Browser = await Playwright.Chromium.LaunchAsync(launchOptions);
                    break;
                case "firefox":
                    Browser = await Playwright.Firefox.LaunchAsync(launchOptions);
                    break;
                case "webkit":
                    Browser = await Playwright.Webkit.LaunchAsync(launchOptions);
                    break;
                case "chromium":
                default:
                    Browser = await Playwright.Chromium.LaunchAsync(launchOptions);
                    break;
            }

            var contextOptions = new BrowserNewContextOptions
            {
                ViewportSize = ViewportSize.NoViewport
            };

            BrowserContext = await Browser!.NewContextAsync(contextOptions);

            BrowserContext!.SetDefaultNavigationTimeout(timeout);
            BrowserContext.SetDefaultTimeout(timeout);

            Page = await BrowserContext.NewPageAsync();
            return Page!;
        }

        public async Task DisposeAsync()
        {
            if (Browser != null)
            {
                await Browser.CloseAsync();
            }

            Playwright?.Dispose();
        }
    }
}