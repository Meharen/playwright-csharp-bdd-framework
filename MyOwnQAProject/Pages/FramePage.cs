using Microsoft.Playwright;
using Enterprise.Automation.Framework.Utilities;

namespace Enterprise.Automation.Framework.PagesAndObjects.Frames
{
    public class FramesPage
    {
        private readonly TestContext _context;
        private readonly FramesObjects _objects;

        public FramesPage(TestContext context)
        {
            _context = context;
            _objects = new FramesObjects(_context.Page!);
        }


        // =========================================================
        // Navigate to The Internet
        // =========================================================

        public async Task NavigateToTheInternetAsync()
        {
            // Use an increased timeout and wait options for navigation; retry once on timeout.
            var page = _context.Page!;
            var url = "https://the-internet.herokuapp.com/";

            for (int attempt = 0; attempt < 2; attempt++)
            {
                try
                {
                    await page.GotoAsync(url, new PageGotoOptions
                    {
                        WaitUntil = WaitUntilState.Load,
                        Timeout = 60000
                    });

                    // Ensure network is idle before proceeding
                    await page.WaitForLoadStateAsync(LoadState.NetworkIdle, new() { Timeout = 60000 });

                    await Assertions.Expect(
                            _objects.FramesLink)
                        .ToBeVisibleAsync(new()
                        {
                            Timeout = 15000
                        });

                    break; // success
                }
                catch when (attempt == 0)
                {
                    // retry once
                    await Task.Delay(1000);
                    continue;
                }
            }
        }


        // =========================================================
        // Open Nested Frames
        // =========================================================

        public async Task OpenNestedFramesAsync()
        {
            await Assertions.Expect(
                    _objects.FramesLink)
                .ToBeVisibleAsync(new()
                {
                    Timeout = 15000
                });

            // Click the Frames link and wait for navigation to complete before looking for Nested Frames link
            await Task.WhenAll(
                _objects.FramesLink.ClickAsync(),
                _context.Page!.WaitForLoadStateAsync(LoadState.NetworkIdle, new() { Timeout = 30000 })
            );

            await Assertions.Expect(
                    _objects.NestedFramesLink)
                .ToBeVisibleAsync(new()
                {
                    Timeout = 30000
                });

            await Task.WhenAll(
                _objects.NestedFramesLink.ClickAsync(),
                _context.Page.WaitForLoadStateAsync(LoadState.NetworkIdle, new() { Timeout = 30000 })
            );


            // Wait until the top frame is available
            await _context.Page.WaitForSelectorAsync(
                "frame[name='frame-top']",
                new()
                {
                    State = WaitForSelectorState.Attached,
                    Timeout = 15000
                });
        }


        // =========================================================
        // Verify LEFT
        // =========================================================

        public async Task VerifyLeftTextAsync()
        {
            await Assertions.Expect(
                    _objects.LeftText)
                .ToBeVisibleAsync(new()
                {
                    Timeout = 15000
                });

            await Assertions.Expect(
                    _objects.LeftText)
                .ToHaveTextAsync(
                    "LEFT",
                    new()
                    {
                        Timeout = 15000
                    });
        }


        // =========================================================
        // Verify MIDDLE
        // =========================================================

        public async Task VerifyMiddleTextAsync()
        {
            await Assertions.Expect(
                    _objects.MiddleText)
                .ToBeVisibleAsync(new()
                {
                    Timeout = 15000
                });

            await Assertions.Expect(
                    _objects.MiddleText)
                .ToHaveTextAsync(
                    "MIDDLE",
                    new()
                    {
                        Timeout = 15000
                    });
        }
    }
}