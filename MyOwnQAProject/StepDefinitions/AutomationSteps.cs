using Enterprise.Automation.Framework.Drivers;
using Enterprise.Automation.Framework.Models; 
using Enterprise.Automation.Framework.Pages;
using Enterprise.Automation.Framework.Utilities;
using Reqnroll;
using System.Threading.Tasks;

namespace AutomationFrameWork.StepDefinitions
{
    [Binding]
    public class AutomationSteps
    {
        private readonly Enterprise.Automation.Framework.Utilities.TestContext _testContext;
        private readonly AutomationPage _automationPage;

        public AutomationSteps(
            PlaywrightDriver playwrightDriver,
            ScenarioContext scenarioContext)
        {
            _testContext = new Enterprise.Automation.Framework.Utilities.TestContext(
                playwrightDriver.Page,
                scenarioContext);

            _automationPage = new AutomationPage(_testContext);
        }

        [Given(@"user navigates to Automation website")]
        public async Task GivenUserNavigatesToAutomationWebsite()
        {
            var configReader = _testContext.ScenarioContext
                .Get<ConfigReader>("ConfigReader");

            await _testContext.Page!
                .GotoAsync(configReader.Urls.Automation);
        }

        [When(@"user fills the automation form")]
        public async Task WhenUserFillsTheAutomationForm()
        {
            await _automationPage.FillBasicDetailsAsync(
                "mehar",
                "mehar@gmail.com",
                "9392854929",
                "pasumamula hyderabad");

            await _automationPage.SelectFemaleAsync();
        }

        [Then(@"gender should be selected")]
        public async Task ThenGenderShouldBeSelected()
        {
            await Task.CompletedTask;
        }
    }
}