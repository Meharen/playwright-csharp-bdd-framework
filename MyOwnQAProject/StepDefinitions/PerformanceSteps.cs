using Enterprise.Automation.Framework.Drivers;
using Enterprise.Automation.Framework.PagesAndObjects.Performance;
using Enterprise.Automation.Framework.Utilities;
using Reqnroll;

namespace Enterprise.Automation.Framework.StepDefinitions
{
    [Binding]
    public class PerformanceSteps
    {
        private readonly TestContext _testContext;
        private readonly PerformancePage _performancePage;

        public PerformanceSteps(
            PlaywrightDriver playwrightDriver,
            ScenarioContext scenarioContext)
        {
            _testContext = new TestContext(
                playwrightDriver.Page,
                scenarioContext);

            _performancePage =
                new PerformancePage(_testContext);
        }

        [When(@"user searches employee performance")]
        public async Task WhenUserSearchesEmployeePerformance()
        {
            await _performancePage.SearchEmployeePerformanceAsync();
        }
    }
}