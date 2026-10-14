using Microsoft.Playwright;

namespace Enterprise.Automation.Framework.Objects
{
    public class AutomationObjects
    {
        private readonly IPage _page;

        public AutomationObjects(IPage page)
        {
            _page = page;
        }

        public ILocator Name =>
            _page.GetByRole(
                AriaRole.Textbox,
                new() { Name = "Enter Name" });

        public ILocator Email =>
            _page.GetByRole(
                AriaRole.Textbox,
                new() { Name = "Enter EMail" });

        public ILocator Phone =>
            _page.GetByRole(
                AriaRole.Textbox,
                new() { Name = "Enter Phone" });

        public ILocator Address =>
            _page.GetByRole(
                AriaRole.Textbox,
                new() { Name = "Address:" });

        public ILocator Female =>
            _page.GetByRole(
                AriaRole.Radio,
                new() { Name = "Female" });
    }
}