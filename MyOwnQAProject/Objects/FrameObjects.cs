using Microsoft.Playwright;

namespace Enterprise.Automation.Framework.PagesAndObjects.Frames
{
    public class FramesObjects
    {
        private readonly IPage _page;

        public FramesObjects(IPage page)
        {
            _page = page;
        }


        // =========================================================
        // Frames Page
        // =========================================================

        public ILocator FramesLink =>
            _page.GetByRole(
                AriaRole.Link,
                new()
                {
                    Name = "Frames",
                    Exact = true
                });


        // =========================================================
        // Nested Frames
        // =========================================================

        public ILocator NestedFramesLink =>
            // Use a resilient locator for the Nested Frames link; some pages render links without accessible names
            // so fall back to a CSS selector that matches the anchor by visible text.
            _page.Locator("a:has-text(\"Nested Frames\")").First;


        // =========================================================
        // Top Frame
        // =========================================================

        public IFrameLocator TopFrame =>
            _page.FrameLocator(
                "frame[name='frame-top']");


        // =========================================================
        // Left Frame
        // =========================================================

        public IFrameLocator LeftFrame =>
            TopFrame.FrameLocator(
                "frame[name='frame-left']");


        // =========================================================
        // Right Frame
        // =========================================================

        public IFrameLocator RightFrame =>
            TopFrame.FrameLocator(
                "frame[name='frame-right']");


        // =========================================================
        // Middle Frame
        // =========================================================

        public IFrameLocator MiddleFrame =>
            TopFrame.FrameLocator(
                "frame[name='frame-middle']");


        // =========================================================
        // LEFT Text
        // =========================================================

        public ILocator LeftText =>
            LeftFrame.GetByText(
                "LEFT",
                new()
                {
                    Exact = true
                });


        // =========================================================
        // MIDDLE Text
        // =========================================================

        public ILocator MiddleText =>
            MiddleFrame.GetByText(
                "MIDDLE",
                new()
                {
                    Exact = true
                });
    }
}