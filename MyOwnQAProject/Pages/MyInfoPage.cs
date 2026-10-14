using Microsoft.Playwright;
using Enterprise.Automation.Framework.Utilities;

namespace Enterprise.Automation.Framework.PagesAndObjects.MyInfo
{
    public class MyInfoPage
    {
        private readonly TestContext _context;
        private readonly MyInfoObjects _objects;

        public MyInfoPage(TestContext context)
        {
            _context = context;
            _objects = new MyInfoObjects(_context.Page!);
        }


        // =========================================================
        // Main Test Flow
        // =========================================================

        public async Task UpdateMyInfoAndUploadResumeAsync()
        {
            // -----------------------------------------------------
            // Verify Dashboard
            // -----------------------------------------------------

            var dashboardHeader =
                _context.Page!.GetByRole(
                    AriaRole.Heading,
                    new()
                    {
                        Name = "Dashboard"
                    });

            await Assertions.Expect(dashboardHeader)
                .ToBeVisibleAsync(new()
                {
                    Timeout = 15000
                });


            // -----------------------------------------------------
            // Click My Info
            // -----------------------------------------------------

            await Assertions.Expect(_objects.MyInfoLink)
                .ToBeVisibleAsync(new()
                {
                    Timeout = 15000
                });

            await _objects.MyInfoLink.ClickAsync();


            // -----------------------------------------------------
            // Wait for First Name
            // -----------------------------------------------------

            await Assertions.Expect(_objects.FirstName)
                .ToBeVisibleAsync(new()
                {
                    Timeout = 15000
                });


            // =====================================================
            // First Name
            // =====================================================

            await _objects.FirstName.FillAsync("Mehar");


            // =====================================================
            // Employee ID / Target Textbox
            // =====================================================

            await Assertions.Expect(_objects.EmployeeId)
                .ToBeVisibleAsync(new()
                {
                    Timeout = 10000
                });

            await _objects.EmployeeId.FillAsync("1478654");


            // =====================================================
            // Add Attachment
            // =====================================================

            await Assertions.Expect(_objects.AddButton)
                .ToBeVisibleAsync(new()
                {
                    Timeout = 10000
                });

            await _objects.AddButton.ClickAsync();


            // =====================================================
            // Upload Resume
            // =====================================================

            await UploadResumeAsync();


            // =====================================================
            // Save
            // =====================================================

            await SaveAsync();
        }


        // =========================================================
        // Upload Resume
        // =========================================================

        private async Task UploadResumeAsync()
        {
            string fileName =
                "Sample_Resume.pdf";

            string filePath = Path.Combine(
                AppDomain.CurrentDomain.BaseDirectory,
                "TestData",
                fileName);


            // -----------------------------------------------------
            // Verify file exists
            // -----------------------------------------------------

            if (!File.Exists(filePath))
            {
                throw new FileNotFoundException(
                    $"Resume file was not found. " +
                    $"Expected path: {filePath}");
            }


            // -----------------------------------------------------
            // Wait for file input
            // -----------------------------------------------------

            await _objects.FileInput.WaitForAsync(
                new LocatorWaitForOptions
                {
                    State = WaitForSelectorState.Attached,
                    Timeout = 10000
                });


            // -----------------------------------------------------
            // Upload directly to input[type=file]
            // -----------------------------------------------------

            await _objects.FileInput.SetInputFilesAsync(
                filePath);
        }


        // =========================================================
        // Save
        // =========================================================

        private async Task SaveAsync()
        {
            await Assertions.Expect(_objects.SaveButtons.Last)
                .ToBeVisibleAsync(new()
                {
                    Timeout = 10000
                });


            await _objects.SaveButtons.Last.ClickAsync();


            // -----------------------------------------------------
            // Wait until page/network activity settles
            // -----------------------------------------------------

            await _context.Page!.WaitForLoadStateAsync(
                LoadState.NetworkIdle);
        }
    }
}