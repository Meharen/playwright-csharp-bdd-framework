using AventStack.ExtentReports;

namespace Enterprise.Automation.Framework.Utilities
{
    public static class ExtentTestManager
    {
        private static readonly ThreadLocal<ExtentTest> _extentTest = new ThreadLocal<ExtentTest>();

        public static ExtentTest? Test
        {
            get => _extentTest.Value;
            set => _extentTest.Value = value!;
        }
    }
}