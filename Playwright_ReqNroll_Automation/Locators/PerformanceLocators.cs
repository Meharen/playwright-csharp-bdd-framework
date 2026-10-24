using Microsoft.Playwright;

namespace Enterprise.Automation.Framework.Locators
{
    public class PerformanceLocators
    {
        private readonly IPage _page;

        public PerformanceLocators(IPage page)
        {
            _page = page;
        }

        // Performance menu
        public ILocator PerformanceLink =>
            _page.GetByRole(
                AriaRole.Link,
                new() { Name = "Performance" });

        // Employee name
        public ILocator EmployeeName =>
            _page.GetByRole(
                AriaRole.Textbox,
                new() { Name = "Type for hints..." });

        // Employee suggestion
        public ILocator EmployeeSuggestion(string employeeName) =>
            _page.GetByText(
                employeeName,
                new() { Exact = true });

        // Dropdowns
        public ILocator SelectDropdowns =>
            _page.GetByText(
                "-- Select --",
                new() { Exact = true });

        // Current Employees Only
        public ILocator CurrentEmployeesOnly =>
            _page.GetByText(
                "Current Employees Only",
                new() { Exact = true });

        // Date fields
        public ILocator DateFields =>
            _page.GetByRole(
                AriaRole.Textbox,
                new() { Name = "yyyy-dd-mm" });

        public ILocator FromDate =>
            DateFields.First;

        public ILocator ToDate =>
            DateFields.Nth(1);

        // Search
        public ILocator SearchButton =>
            _page.GetByRole(
                AriaRole.Button,
                new() { Name = "Search" });
    }
}