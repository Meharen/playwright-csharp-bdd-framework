using AventStack.ExtentReports;
using AventStack.ExtentReports.Reporter;

namespace Enterprise.Automation.Framework.Utilities
{
    public class ExtentReportManager
    {
        private static ExtentReports? _extent;

        public static ExtentReports GetExtentReport()
        {
            if (_extent == null)
            {
                string reportPath = Path.Combine(Directory.GetCurrentDirectory(), "TestResults", "ExtentReport.html");
                var sparkReporter = new ExtentSparkReporter(reportPath);

                _extent = new ExtentReports();
                _extent.AttachReporter(sparkReporter);
            }
            return _extent;
        }
    }
}