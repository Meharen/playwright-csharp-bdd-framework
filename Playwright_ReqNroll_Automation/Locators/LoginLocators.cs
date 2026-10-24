using Microsoft.Playwright;

namespace Enterprise.Automation.Framework.Locators
{
    public class LoginLocators
    {
        private readonly IPage _page;

        public LoginLocators(IPage page)
        {
            _page = page;
        }

        public ILocator Username =>
            _page.GetByRole(
                AriaRole.Textbox,
                new() { Name = "Username" });

        public ILocator Password =>
            _page.GetByRole(
                AriaRole.Textbox,
                new() { Name = "Password" });

        public ILocator LoginButton =>
            _page.GetByRole(
                AriaRole.Button,
                new() { Name = "Login" });

        public ILocator DashboardHeader =>
            _page.GetByRole(
                AriaRole.Heading,
                new() { Name = "Dashboard" });
    }
}