using System.Threading.Tasks;
using Microsoft.Playwright;
using Enterprise.Automation.Framework.Models;
using Enterprise.Automation.Framework.Locators;

namespace Enterprise.Automation.Framework.Pages
{
    public class RoboFormPage
    {
        private readonly IPage _page;
        private readonly RoboFormLocators _objects;

        public RoboFormPage(IPage page)
        {
            _page = page;
            _objects = new RoboFormLocators(_page);
        }

        public async Task FillFormAsync(RoboFormData data)
        {
            await _objects.Title.FillAsync(data.Title);
            await _objects.FirstName.FillAsync(data.FirstName);
            await _objects.MiddleName.FillAsync(data.MiddleName);
            await _objects.LastName.FillAsync(data.LastName);
            await _objects.FullName.FillAsync(data.FullName);

            await _objects.Company.FillAsync(data.Company);
            await _objects.Position.FillAsync(data.Position);

            await _objects.Address1.FillAsync(data.Address1);
            await _objects.Address2.FillAsync(data.Address2);
            await _objects.City.FillAsync(data.City);
            await _objects.State.FillAsync(data.State);
            await _objects.Country.FillAsync(data.Country);
            await _objects.ZipCode.FillAsync(data.ZipCode);

            await _objects.HomePhone.FillAsync(data.HomePhone);
            await _objects.WorkPhone.FillAsync(data.WorkPhone);
            await _objects.FaxPhone.FillAsync(data.FaxPhone);
            await _objects.CellPhone.FillAsync(data.CellPhone);

            await _objects.Email.FillAsync(data.Email);
            await _objects.Website.FillAsync(data.Website);

            await _objects.UserId.FillAsync(data.UserId);
            await _objects.Password.FillAsync(data.Password);

            try
            {
                string rawCard = data.CardType?.ToString().Trim() ?? "";
                await _objects.CardType.SelectOptionAsync(new[] {
                    new SelectOptionValue { Label = rawCard },
                    new SelectOptionValue { Value = rawCard },
                    new SelectOptionValue { Label = rawCard.Replace(" ", "") },
                    new SelectOptionValue { Value = rawCard.Replace(" ", "") }
                });
            }
            catch
            {
                await _objects.CardType.SelectOptionAsync(new SelectOptionValue { Index = 1 });
            }

            try
            {
                string rawMonth = data.ExpMonth?.ToString().Trim() ?? "";
                await _page.SelectOptionAsync("select[name*='month'], select[id*='month'], select[name*='Month']", new[] {
                    new SelectOptionValue { Label = rawMonth },
                    new SelectOptionValue { Value = rawMonth }
                });
            }
            catch
            {
                try { await _objects.ExpMonth.SelectOptionAsync(new SelectOptionValue { Index = 1 }); } catch { }
            }

            try
            {
                string rawYear = data.ExpYear?.ToString().Trim() ?? "";
                await _page.SelectOptionAsync("select[name*='year'], select[id*='year'], select[name*='Year']", new[] {
                    new SelectOptionValue { Label = rawYear },
                    new SelectOptionValue { Value = rawYear }
                });
            }
            catch
            {
                try { await _objects.ExpYear.SelectOptionAsync(new SelectOptionValue { Index = 1 }); } catch { }
            }

            await _objects.CardNumber.FillAsync(data.CardNumber);
            await _objects.Cvv.FillAsync(data.Cvv);
            await _objects.CardUserName.FillAsync(data.CardUserName);
            await _objects.CardIssuer.FillAsync(data.CardIssuer);
            await _objects.CustomerService.FillAsync(data.CustomerService);

            await _objects.Gender.FillAsync(data.Gender);
            await _objects.SSN.FillAsync(data.SSN);
            await _objects.DrivingLicense.FillAsync(data.DrivingLicense);

            await _objects.Age.FillAsync(data.Age);
            await _objects.BirthPlace.FillAsync(data.BirthPlace);
            await _objects.Income.FillAsync(data.Income);

            await _objects.CustomField.FillAsync(data.CustomField);
            await _objects.Comments.FillAsync(data.Comments);

            await Task.Delay(5000);

            await _objects.ResetButton.ClickAsync();

            await Task.Delay(1000);
        }
    }
}