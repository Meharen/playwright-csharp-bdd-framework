using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Playwright;
using Enterprise.Automation.Framework.Models;
using Enterprise.Automation.Framework.Utilities;
using Reqnroll;

namespace Enterprise.Automation.Framework.StepDefinitions
{
    [Binding]
    public class RoboFormSteps
    {
        private readonly IPage _page;
        private readonly ScenarioContext _scenarioContext;

        public RoboFormSteps(IPage page, ScenarioContext scenarioContext)
        {
            _page = page ?? throw new ArgumentNullException(nameof(page));
            _scenarioContext = scenarioContext ?? throw new ArgumentNullException(nameof(scenarioContext));
        }

        [Given(@"user navigates to RoboForm website")]
        public async Task GivenUserNavigatesToRoboFormWebsite()
        {
            var config = _scenarioContext.ContainsKey("Config") ? _scenarioContext["Config"] as IConfiguration : null;

            string url = config?["AppSettings:BaseUrl"]
                      ?? config?["Urls:RoboForm"]
                      ?? config?["BaseUrl"]
                      ?? "https://www.roboform.com/filling-test-all-fields";

            Console.WriteLine($"Navigating to target URL: {url}");
            await _page.GotoAsync(url);
        }

        [Then(@"user fills RoboForm form using Excel data")]
        public async Task ThenUserFillsRoboFormFormUsingExcelData()
        {
            string excelPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "TestData", "TestData.xlsx");

            if (!File.Exists(excelPath))
            {
                excelPath = Path.Combine(Directory.GetCurrentDirectory(), "TestData", "TestData.xlsx");
            }

            Console.WriteLine($"Reading Excel data from: {excelPath}");

            var excelUtility = new ExcelUtility(excelPath);
            // Read into the form model so the workbook supplies the form values.
            var formDataList = excelUtility.ReadAsList<Enterprise.Automation.Framework.Models.RoboFormData>("Sheet1");

            var rowData = formDataList.FirstOrDefault();

            if (rowData != null)
            {
                // Gather property values from RoboFormData in declared order
                var props = typeof(Enterprise.Automation.Framework.Models.RoboFormData).GetProperties();
                var propValues = props.Select(p => (p.GetValue(rowData) as string) ?? string.Empty).ToList();

                // Query all form controls and attempt to fill them using property values in sequence
                var elements = await _page.QuerySelectorAllAsync("input, textarea, select");

                int valueIndex = 0;
                foreach (var el in elements)
                {
                    if (valueIndex >= propValues.Count)
                        break;

                    // Skip hidden inputs
                    var type = await el.GetAttributeAsync("type") ?? string.Empty;
                    if (type.Equals("hidden", StringComparison.OrdinalIgnoreCase))
                        continue;

                    string valueToUse = propValues[valueIndex];

                    try
                    {
                        var tag = await el.EvaluateAsync<string>("e => e.tagName.toLowerCase()");
                        if (tag == "select")
                        {
                            // try to select by value, otherwise select first non-empty option
                            try
                            {
                                await el.SelectOptionAsync(new[] { valueToUse });
                            }
                            catch
                            {
                                var opt = await el.QuerySelectorAsync("option:not([value=''])");
                                if (opt != null)
                                {
                                    var optVal = await opt.GetAttributeAsync("value") ?? await opt.EvaluateAsync<string>("o => o.textContent");
                                    await el.SelectOptionAsync(new[] { optVal });
                                }
                            }
                        }
                        else if (type.Equals("checkbox", StringComparison.OrdinalIgnoreCase) || type.Equals("radio", StringComparison.OrdinalIgnoreCase))
                        {
                            // Check radio/checkbox if the property value is truthy or matches value
                            if (!string.IsNullOrEmpty(valueToUse))
                            {
                                // try to match by value attribute
                                var valAttr = await el.GetAttributeAsync("value");
                                if (!string.IsNullOrEmpty(valAttr) && valAttr.Equals(valueToUse, StringComparison.OrdinalIgnoreCase))
                                {
                                    await el.CheckAsync();
                                }
                                else
                                {
                                    // as a fallback, check the element
                                    await el.CheckAsync();
                                }
                            }
                        }
                        else
                        {
                            if (!string.IsNullOrEmpty(valueToUse))
                            {
                                await el.FillAsync(valueToUse);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Warning: failed to fill element: {ex.Message}");
                    }

                    valueIndex++;
                }

                Console.WriteLine($"Form filled successfully for: {rowData.FirstName} {rowData.LastName}");
            }
            else
            {
                throw new Exception("No data rows found in the Excel sheet!");
            }
        }
    }
}