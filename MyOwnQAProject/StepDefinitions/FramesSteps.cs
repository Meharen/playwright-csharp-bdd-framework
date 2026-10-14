using Enterprise.Automation.Framework.Drivers;
using Enterprise.Automation.Framework.PagesAndObjects.Frames;
using Enterprise.Automation.Framework.Utilities;
using Reqnroll;

namespace Enterprise.Automation.Framework.StepDefinitions
{
    [Binding]
    public class FramesSteps
    {
        private readonly TestContext _testContext;
        private readonly FramesPage _framesPage;

        public FramesSteps(
            PlaywrightDriver playwrightDriver,
            ScenarioContext scenarioContext)
        {
            _testContext = new TestContext(
                playwrightDriver.Page,
                scenarioContext);

            _framesPage = new FramesPage(_testContext);
        }


        [Given(@"user navigates to The Internet website")]
        public async Task GivenUserNavigatesToTheInternetWebsite()
        {
            await _framesPage.NavigateToTheInternetAsync();
        }


        [When(@"user opens Nested Frames")]
        public async Task WhenUserOpensNestedFrames()
        {
            await _framesPage.OpenNestedFramesAsync();
        }


        [Then(@"user verifies LEFT text in left frame")]
        public async Task ThenUserVerifiesLeftTextInLeftFrame()
        {
            await _framesPage.VerifyLeftTextAsync();
        }


        [Then(@"user verifies MIDDLE text in middle frame")]
        public async Task ThenUserVerifiesMiddleTextInMiddleFrame()
        {
            await _framesPage.VerifyMiddleTextAsync();
        }
    }
}