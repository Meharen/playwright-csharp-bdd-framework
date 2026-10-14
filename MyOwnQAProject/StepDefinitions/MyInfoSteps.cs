using Enterprise.Automation.Framework.Drivers;
using Enterprise.Automation.Framework.PagesAndObjects.MyInfo;
using Enterprise.Automation.Framework.Utilities;
using Reqnroll;

namespace Enterprise.Automation.Framework.StepDefinitions
{
    [Binding]
    public class MyInfoSteps
    {
        private readonly TestContext _testContext;
        private readonly MyInfoPage _myInfoPage;

        public MyInfoSteps(
            PlaywrightDriver playwrightDriver,
            ScenarioContext scenarioContext)
        {
            _testContext = new TestContext(
                playwrightDriver.Page,
                scenarioContext);

            _myInfoPage = new MyInfoPage(_testContext);
        }

        [When(@"user updates My Info and uploads resume")]
        public async Task WhenUserUpdatesMyInfoAndUploadsResume()
        {
            await _myInfoPage.UpdateMyInfoAndUploadResumeAsync();
        }
    }
}