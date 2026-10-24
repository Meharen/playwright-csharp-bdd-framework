using Enterprise.Automation.Framework.Drivers;
using Enterprise.Automation.Framework.Pages;
using Enterprise.Automation.Framework.Utilities;
using Reqnroll;

namespace Enterprise.Automation.Framework.StepDefinitions
{
    [Binding]
    public class LoginSteps
    {
        private readonly TestContext _testContext;
        private readonly LoginPage _loginPage;

        public LoginSteps(
            PlaywrightDriver playwrightDriver,
            ScenarioContext scenarioContext)
        {
            _testContext = new TestContext(
                playwrightDriver.Page,
                scenarioContext);

            _loginPage = new LoginPage(_testContext);
        }

        [Given(@"user navigates to OrangeHRM login page")]
        public async Task GivenUserNavigatesToOrangeHRMLoginPage()
        {
            var configReader =
                _testContext.ScenarioContext
                    .Get<ConfigReader>("ConfigReader");

            await _loginPage.NavigateAsync(
                configReader.Urls.PIM);
        }

        [When(@"user logs in with valid credentials")]
        public async Task WhenUserLogsInWithValidCredentials()
        {
            var configReader =
                _testContext.ScenarioContext
                    .Get<ConfigReader>("ConfigReader");

            await _loginPage.LoginAsync(
                configReader.Username,
                configReader.Password);
        }

        [When("user logs in with username {string} and password {string}")]
        public async Task WhenUserLogsInWithUsernameAndPassword(string username, string password)
        {
            await _loginPage.LoginAsync(username, password);
        }

        [Then(@"Dashboard header should be displayed")]
        public async Task ThenDashboardHeaderShouldBeDisplayed()
        {
            await _loginPage.ValidateDashboardAsync();
        }
    }
}