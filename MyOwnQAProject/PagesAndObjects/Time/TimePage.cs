using Microsoft.Playwright;
using TestContext = Enterprise.Automation.Framework.Utilities.TestContext;

namespace Enterprise.Automation.Framework.PagesAndObjects.Time
{
    public class TimePage
    {
        private readonly TestContext _context;
        private readonly TimeObjects _objects;

        public TimePage(TestContext context)
        {
            _context = context;
            _objects = new TimeObjects(_context.Page!);
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
