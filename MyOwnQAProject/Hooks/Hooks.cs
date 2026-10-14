using AventStack.ExtentReports;
using Microsoft.Extensions.Configuration;
using Enterprise.Automation.Framework.Drivers;
using Enterprise.Automation.Framework.Utilities;
using Reqnroll;
using Reqnroll.BoDi;
using System;
using System.IO;
using System.Threading.Tasks;

namespace Enterprise.Automation.Framework.Hooks
{
    [Binding]
    public class Hooks
    {
        private readonly PlaywrightDriver _playwrightDriver;
        private readonly ScenarioContext _scenarioContext;
        private readonly FeatureContext _featureContext;
        private readonly IObjectContainer _objectContainer;

        private static ExtentReports? _extentReports;
        private static ExtentTest? _extentFeature;
        private ExtentTest? _extentScenario;

        public Hooks(
            PlaywrightDriver playwrightDriver,
            ScenarioContext scenarioContext,
            FeatureContext featureContext,
            IObjectContainer objectContainer)
        {
            _playwrightDriver = playwrightDriver ?? throw new ArgumentNullException(nameof(playwrightDriver));
            _scenarioContext = scenarioContext ?? throw new ArgumentNullException(nameof(scenarioContext));
            _featureContext = featureContext ?? throw new ArgumentNullException(nameof(featureContext));
            _objectContainer = objectContainer ?? throw new ArgumentNullException(nameof(objectContainer));
        }

        [BeforeTestRun]
        public static void BeforeTestRun()
        {
            _extentReports = ExtentReportManager.GetExtentReport();
        }

        [AfterTestRun]
        public static void AfterTestRun()
        {
            _extentReports?.Flush();
        }

        [BeforeFeature]
        public static void BeforeFeature(FeatureContext featureContext)
        {
            if (_extentReports != null)
            {
                var feature = _extentReports.CreateTest(featureContext.FeatureInfo.Title);
                featureContext["ExtentFeature"] = feature;
            }
        }

        [BeforeScenario(Order = 0)]
        public async Task BeforeScenarioAsync()
        {
            LoadConfiguration();

            if (_featureContext.TryGetValue("ExtentFeature", out ExtentTest storedFeature))
            {
                _extentFeature = storedFeature;
                _extentScenario = _extentFeature.CreateNode(_scenarioContext.ScenarioInfo.Title);
                _scenarioContext["ExtentScenario"] = _extentScenario;
            }

            await _playwrightDriver.InitializeAsync();

            if (_playwrightDriver.Page != null)
            {
                _objectContainer.RegisterInstanceAs(_playwrightDriver.Page);
            }
        }

        [AfterScenario]
        public async Task AfterScenarioAsync()
        {
            await _playwrightDriver.DisposeAsync();
        }

        [AfterStep]
        public async Task AfterStep(ScenarioContext scenarioContext)
        {
            var stepText = scenarioContext.StepContext.StepInfo.Text;
            var stepType = scenarioContext.StepContext.StepInfo.StepDefinitionType;

            if (scenarioContext.TryGetValue("ExtentScenario", out ExtentTest extentScenario))
            {
                if (scenarioContext.TestError == null)
                {
                    extentScenario.Pass($"{stepType} {stepText}");
                }
                else
                {
                    var base64Screenshot = await CaptureScreenshotAsBase64Async();
                    if (!string.IsNullOrEmpty(base64Screenshot))
                    {
                        extentScenario.Fail($"{stepType} {stepText} <br>* Reason: {scenarioContext.TestError.Message}",
                            MediaEntityBuilder.CreateScreenCaptureFromBase64String(base64Screenshot).Build());
                    }
                    else
                    {
                        extentScenario.Fail($"{stepType} {stepText} <br>* Reason: {scenarioContext.TestError.Message}");
                    }
                }
            }
        }

        private void LoadConfiguration()
        {
            var projectRoot = Directory.GetCurrentDirectory();
            var configuration = new ConfigurationBuilder()
                .SetBasePath(projectRoot)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .Build();

            // ConfigReader setup & JSON Binding
            var configReader = new ConfigReader();
            configuration.Bind(configReader);

            // Fallback URL binding logic in case appsettings.json file empty or missing
            if (configReader.Urls == null)
            {
                configReader.Urls = new UrlSettings();
            }

            if (string.IsNullOrEmpty(configReader.Urls.Automation))
            {
                configReader.Urls.Automation = "https://demo.automationtesting.in/Register.html";
            }

            // ScenarioContext and BoDi container registration
            _scenarioContext["Config"] = configuration;
            _scenarioContext["ConfigReader"] = configReader;
            _objectContainer.RegisterInstanceAs(configReader);
        }

        private async Task<string?> CaptureScreenshotAsBase64Async()
        {
            try
            {
                if (_playwrightDriver.Page != null)
                {
                    var screenshotBytes = await _playwrightDriver.Page.ScreenshotAsync(new() { FullPage = true });
                    return Convert.ToBase64String(screenshotBytes);
                }
                return null;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Screenshot capture failed: " + ex.Message);
                return null;
            }
        }
    }
}