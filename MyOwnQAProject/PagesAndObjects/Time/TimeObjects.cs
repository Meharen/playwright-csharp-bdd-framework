using Microsoft.Playwright;

namespace Enterprise.Automation.Framework.PagesAndObjects.Time
{
    public class TimeObjects
    {
        private readonly IPage _page;

        public TimeObjects(IPage page)
        {
            _page = page;
        }

        public ILocator TimeLink =>
            _page.GetByRole(
                AriaRole.Link,
                new() { Name = "Time" });

        public ILocator SearchTextbox =>
            _page.GetByRole(
                AriaRole.Textbox,
                new() { Name = "Type for hints..." });

        public ILocator ViewButton =>
            _page.Locator("form")
                 .GetByRole(
                     AriaRole.Button,
                     new() { Name = "View" });

        public ILocator InvalidMessage =>
            _page.GetByText("Invalid");
    }
}
