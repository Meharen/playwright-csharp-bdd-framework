using Microsoft.Playwright;
using Enterprise.Automation.Framework.Locators;
using TestContext = Enterprise.Automation.Framework.Utilities.TestContext;

namespace Enterprise.Automation.Framework.Pages
{
    public class TimePage
    {
        private readonly TestContext _context;
        private readonly TimeLocators _objects;

        public TimePage(TestContext context)
        {
            _context = context;
            _objects = new TimeLocators(_context.Page!);
        }

        public async Task ClickTimeLinkAsync()
        {
            await _objects.TimeLink.ClickAsync();
            await Task.Delay(2000);
        }

        public async Task SearchAkhilAndClickViewAsync()
        {
            await _objects.SearchTextbox.ClickAsync();
            await Task.Delay(500);

            await _objects.SearchTextbox.FillAsync("akhil");
            await Task.Delay(1000);

            await _objects.ViewButton.ClickAsync();
            await Task.Delay(2000);
        }

        public async Task ValidateInvalidMessageAsync()
        {
            await Assertions
                .Expect(_objects.InvalidMessage)
                .ToBeVisibleAsync(new() { Timeout = 15000 });
        }
    }
}
