using Microsoft.Playwright;
using Enterprise.Automation.Framework.Locators;
using Enterprise.Automation.Framework.Utilities;

namespace Enterprise.Automation.Framework.Pages
{
    public class PerformancePage
    {
        private readonly TestContext _context;
        private readonly PerformanceLocators _objects;

        public PerformancePage(TestContext context)
        {
            _context = context;
            _objects = new PerformanceLocators(_context.Page!);
        }

        public async Task SearchEmployeePerformanceAsync()
        {
            // --------------------------------------------------
            // 1. Open Performance
            // --------------------------------------------------

            await _objects.PerformanceLink.ClickAsync();

            await _objects.EmployeeName.WaitForAsync(
                new LocatorWaitForOptions
                {
                    State = WaitForSelectorState.Visible
                });

            // --------------------------------------------------
            // 2. Search employee
            // --------------------------------------------------

            await _objects.EmployeeName.FillAsync("Mehar");

            // Wait for employee suggestions
            await _context.Page!.WaitForTimeoutAsync(1000);

            // Select employee
            var employeeSuggestion =
                _objects.EmployeeSuggestion("Mehar");

            if (await employeeSuggestion.CountAsync() > 0)
            {
                await employeeSuggestion.First.ClickAsync();
            }

            // --------------------------------------------------
            // 3. First dropdown
            // --------------------------------------------------

            var dropdowns = _objects.SelectDropdowns;

            await dropdowns.First.ClickAsync();

            await _context.Page
                .GetByText("-- Select --", new() { Exact = true })
                .First.ClickAsync();

            // --------------------------------------------------
            // 4. Current Employees Only
            // --------------------------------------------------

            await _objects.CurrentEmployeesOnly.ClickAsync();

            // --------------------------------------------------
            // 5. Second dropdown
            // --------------------------------------------------

            var remainingDropdowns =
                _objects.SelectDropdowns;

            if (await remainingDropdowns.CountAsync() > 0)
            {
                await remainingDropdowns.First.ClickAsync();
            }

            // --------------------------------------------------
            // 6. From Date
            // --------------------------------------------------

            await _objects.FromDate.FillAsync("2026-05-10");

            // --------------------------------------------------
            // 7. To Date
            // --------------------------------------------------

            await _objects.ToDate.FillAsync("2026-12-20");

            // --------------------------------------------------
            // 8. Search
            // --------------------------------------------------

            await _objects.SearchButton.ClickAsync();

            // Wait until search operation completes
            await _context.Page.WaitForLoadStateAsync(
                LoadState.NetworkIdle);
        }
    }
}