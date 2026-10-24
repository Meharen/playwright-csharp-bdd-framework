using Microsoft.Playwright;
using Reqnroll;


namespace Enterprise.Automation.Framework.Utilities
{
    public class TestContext
    {
        public IPage? Page { get; }

        public ScenarioContext ScenarioContext { get; }

        public TestContext(
            IPage? page,
            ScenarioContext scenarioContext)
        {
            Page = page;
            ScenarioContext = scenarioContext;
        }
    }
}