using System.Threading.Tasks;
using Enterprise.Automation.Framework.Locators;
using TestContext = Enterprise.Automation.Framework.Utilities.TestContext;
namespace Enterprise.Automation.Framework.Pages
{
    public class AutomationPage
    {
        private readonly TestContext _context;
        private readonly AutomationLocators _objects;

        public AutomationPage(TestContext context)
        {
            _context = context;
            _objects = new AutomationLocators(_context.Page!);
        }

        public async Task FillNameAsync(string name)
        {
            await _objects.Name.FillAsync(name);
        }

        public async Task FillEmailAsync(string email)
        {
            await _objects.Email.FillAsync(email);
        }

        public async Task FillPhoneAsync(string phone)
        {
            await _objects.Phone.FillAsync(phone);
        }

        public async Task FillAddressAsync(string address)
        {
            await _objects.Address.FillAsync(address);
        }

        public async Task SelectFemaleAsync()
        {
            await _objects.Female.CheckAsync();
        }

        public async Task FillBasicDetailsAsync(
            string name,
            string email,
            string phone,
            string address)
        {
            await FillNameAsync(name);
            await FillEmailAsync(email);
            await FillPhoneAsync(phone);
            await FillAddressAsync(address);
        }
    }
}