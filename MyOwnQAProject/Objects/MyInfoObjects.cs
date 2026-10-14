using Microsoft.Playwright;

namespace Enterprise.Automation.Framework.PagesAndObjects.MyInfo
{
    public class MyInfoObjects
    {
        private readonly IPage _page;

        public MyInfoObjects(IPage page)
        {
            _page = page;
        }


        // =========================================================
        // My Info
        // =========================================================

        public ILocator MyInfoLink =>
            _page.GetByRole(
                AriaRole.Link,
                new()
                {
                    Name = "My Info"
                });


        // =========================================================
        // First Name
        // =========================================================

        public ILocator FirstName =>
            _page.GetByRole(
                AriaRole.Textbox,
                new()
                {
                    Name = "First Name"
                });


        // =========================================================
        // Employee ID / Target Textbox
        //
        // Your recorded script uses:
        // getByRole('textbox').nth(4)
        //
        // We keep this locator isolated here instead of putting
        // nth(4) inside the Page class.
        // =========================================================

        public ILocator EmployeeId =>
            _page.GetByRole(
                AriaRole.Textbox)
                .Nth(4);


        // =========================================================
        // Add Button
        // =========================================================

        public ILocator AddButton =>
            _page.GetByRole(
                AriaRole.Button,
                new()
                {
                    Name = " Add"
                });


        // =========================================================
        // File Input
        // =========================================================
        //
        // Do NOT click "Choose File".
        // Playwright can upload directly to input[type=file].
        // =========================================================

        public ILocator FileInput =>
            _page.Locator(
                "input[type='file']");


        // =========================================================
        // Save Button
        // =========================================================

        public ILocator SaveButtons =>
            _page.GetByRole(
                AriaRole.Button,
                new()
                {
                    Name = "Save"
                });
    }
}