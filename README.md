# Enterprise Automation Framework

A C# browser UI automation framework using Playwright, Reqnroll BDD scenarios, and xUnit.

## Tech Stack

- .NET 10 (`net10.0`)
- Microsoft Playwright 1.63.0
- Reqnroll 3.3.4 with Reqnroll.XUnit 3.3.4
- xUnit 2.9.3
- ExtentReports 5.0.4
- ExcelDataReader and ExcelDataReader.DataSet 3.9.0

## What It Demonstrates

- Reqnroll feature files mapped to C# step definitions and page objects.
- Playwright-driven UI scenarios for forms, login, nested frames, employee information, and performance search.
- Excel workbook reading for the RoboForm form scenario.
- Reqnroll hooks that initialize and dispose a browser per scenario, and add step results and failure screenshots to an Extent report.
- The driver contains launch paths for Chromium, Chrome, Edge, Firefox, and WebKit. The current hooks use the driver's defaults (Chromium, headed); the `Browser` and `Headless` values in `appsettings.json` are not currently passed to the driver.

## Project Structure

```text
Enterprise.Automation.Framework.slnx
MyOwnQAProject/                         .NET test project
  Enterprise.Automation.Framework.csproj  Project and NuGet dependencies
  appsettings.json                      Demo credentials, URLs, and settings
  Drivers/                              Playwright browser lifecycle
  Features/                             Reqnroll scenarios and generated code-behind
  Hooks/                                Scenario lifecycle and report integration
  Models/                               Form data model
  Locators/                             Playwright locators
  Pages/                                Page actions for UI scenarios
  StepDefinitions/                      Reqnroll step bindings
  TestData/                             Excel workbook and resume upload fixture
  Tests/                                Reserved test folder
  Utilities/                            Configuration, Excel, context, and report helpers
  Reports/                              Reserved report folder
  Traces/                               Reserved trace folder
```

## Prerequisites

- .NET 10 SDK.
- PowerShell 7 (`pwsh`) to run the generated Playwright browser installer.

From the repository root, build once and install the browsers:

```powershell
dotnet build Enterprise.Automation.Framework.slnx
pwsh MyOwnQAProject/bin/Debug/net10.0/playwright.ps1 install
```

## Configuration

`MyOwnQAProject/appsettings.json` contains `MyOwnQAProjectSettings.Browser`, `Headless`, and `Timeout`; `Credentials.Username` and `Credentials.Password`; demo URLs under `Urls`; `Runtime` execution settings; and `Reporting.Type` and `Reporting.OutputPath`. The password is intentionally not reproduced here.

There are current wiring limitations: `Hooks` binds the root JSON object to `ConfigReader`, whose properties do not include `MyOwnQAProjectSettings`, `Credentials`, `Runtime`, or `Reporting`. Only the root `Urls` section currently binds. The driver therefore uses its defaults (Chromium, headed, 30-second timeout). The login feature supplies its public demo credentials directly in the scenario, while configuration-based credentials remain unbound. The Extent report manager writes to `TestResults/ExtentReport.html` directly rather than reading `Reporting.OutputPath`.

The configured `Urls.RoboForm` points to RoboForm's public form-filling demo. The login features use the public OrangeHRM demo account; keep real credentials out of source-controlled configuration.

## Running Tests

Run all scenarios:

```powershell
dotnet test Enterprise.Automation.Framework.slnx
```

Run a single feature by its generated test name:

```powershell
dotnet test Enterprise.Automation.Framework.slnx --filter "FullyQualifiedName~Login"
```

Run scenarios tagged `@Login`:

```powershell
dotnet test Enterprise.Automation.Framework.slnx --filter "Category=Login"
```

Other feature tags include `@AutomationForm`, `@RoboForm`, `@NestedFrames`, `@MyInfoResumeUpload`, and `@PerformanceSearch`.

## Scenarios

| Feature | Tag | Coverage | Demo site |
| --- | --- | --- | --- |
| Automation | `@AutomationForm` | Fill basic registration fields and select gender | [Test Automation Practice](https://testautomationpractice.blogspot.com/) |
| RoboForm form filling | `@RoboForm` | Fill a form using workbook data | [RoboForm form-filling demo](https://www.roboform.com/filling-test-all-fields) |
| Frames | `@NestedFrames` | Verify LEFT and MIDDLE text in nested frames | [The Internet](https://the-internet.herokuapp.com/) |
| Login | `@Login` | Sign in and verify the Dashboard heading | [OrangeHRM demo](https://opensource-demo.orangehrmlive.com/web/index.php/auth/login) |
| My Info | `@MyInfoResumeUpload` | Update profile fields and upload the sample resume; includes the login background | [OrangeHRM demo](https://opensource-demo.orangehrmlive.com/web/index.php/auth/login) |
| Performance | `@PerformanceSearch` | Search employee performance; includes the login background | [OrangeHRM demo](https://opensource-demo.orangehrmlive.com/web/index.php/auth/login) |

## Reports

The Extent HTML report is written to `MyOwnQAProject/TestResults/ExtentReport.html`. The hooks flush the report after the test run and attach a screenshot when a step fails and screenshot capture succeeds.

## Known Limitations and Planned Improvements

- Fixed waits (`Task.Delay` and `WaitForTimeoutAsync`) remain in several page flows.
- Browser, headless, timeout, runtime, and report settings need to be wired from configuration.
- Generated Reqnroll `.feature.cs` files are ignored but may still be tracked in the current Git index until explicitly untracked.
- There is no CI workflow in the repository.
- The Excel form step reads the first data row and fills controls by page order; it is not a general-purpose record-to-field mapping.

## Author

Shaik Meharunnissa, QA Automation Engineer  
[LinkedIn](https://linkedin.com/in/skmehar20)