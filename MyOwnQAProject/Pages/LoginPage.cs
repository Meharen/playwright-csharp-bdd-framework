using Microsoft.Playwright;
using TestContext = Enterprise.Automation.Framework.Utilities.TestContext;
namespace Enterprise.Automation.Framework.PagesAndObjects.Login
{
    public class LoginPage
    {
        private readonly TestContext _context;
        private readonly LoginObjects _objects;

        public LoginPage(TestContext context)
        {
            _context = context;
            _objects = new LoginObjects(_context.Page!);
        }

        public async Task NavigateAsync(string url)
        {
            if (string.IsNullOrWhiteSpace(url))
            {
                throw new ArgumentException(
                    "Login URL cannot be empty.",
                    nameof(url));
            }

            await _context.Page!.GotoAsync(url);

            await _context.Page!.WaitForLoadStateAsync(
                LoadState.NetworkIdle);
        }

        public async Task LoginAsync(
            string username,
            string password)
        {
            await _objects.Username.FillAsync(username);
            await Task.Delay(1000);

            await _objects.Password.FillAsync(password);
            await Task.Delay(1000);

            // Click the login button and wait for the page to reach NetworkIdle load state instead of using obsolete WaitForNavigationAsync.
            await Task.WhenAll(
                _objects.LoginButton.ClickAsync(),
                _context.Page!.WaitForLoadStateAsync(LoadState.NetworkIdle, new() { Timeout = 15000 })
            );
        }

        public async Task ValidateDashboardAsync()
        {
            await Assertions
                .Expect(_objects.DashboardHeader)
                .ToBeVisibleAsync(new() { Timeout = 15000 });
        }
    }
}