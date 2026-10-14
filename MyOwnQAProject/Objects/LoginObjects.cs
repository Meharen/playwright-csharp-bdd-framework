using Microsoft.Playwright;

namespace Enterprise.Automation.Framework.PagesAndObjects.Login
{
    public class LoginObjects
    {
        private readonly IPage _page;

        public LoginObjects(IPage page)
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