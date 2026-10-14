using Microsoft.Playwright;

namespace Enterprise.Automation.Framework.Objects
{
    public class FlipkartObjects
    {
        private readonly IPage _page;

        public FlipkartObjects(IPage page)
        {
            _page = page;
        }

        public ILocator Title => _page.Locator("input[name='01___title']");
        public ILocator FirstName => _page.Locator("input[name='02frstname']");
        public ILocator MiddleName => _page.Locator("input[name='03middle_i']");
        public ILocator LastName => _page.Locator("input[name='04lastname']");
        public ILocator FullName => _page.Locator("input[name='04fullname']");
        public ILocator Company => _page.Locator("input[name='05_company']");
        public ILocator Position => _page.Locator("input[name='06position']");

        public ILocator Address1 => _page.Locator("input[name='10address1']");
        public ILocator Address2 => _page.Locator("input[name='11address2']");
        public ILocator City => _page.Locator("input[name='13adr_city']");
        public ILocator State => _page.Locator("input[name='14adrstate']");
        public ILocator Country => _page.Locator("input[name='15_country']");
        public ILocator ZipCode => _page.Locator("input[name='16addr_zip']");

        public ILocator HomePhone => _page.Locator("input[name='20homephon']");
        public ILocator WorkPhone => _page.Locator("input[name='21workphon']");
        public ILocator FaxPhone => _page.Locator("input[name='22faxphone']");
        public ILocator CellPhone => _page.Locator("input[name='23cellphon']");

        public ILocator Email => _page.Locator("input[name='24emailadr']");
        public ILocator Website => _page.Locator("input[name='25web_site']");

        public ILocator UserId => _page.Locator("input[name='30_user_id']");
        public ILocator Password => _page.Locator("input[name='31password']");

        public ILocator CardType => _page.Locator("select[name$='__type']");
        public ILocator ExpMonth => _page.Locator("select[name$='__month']");
        public ILocator ExpYear => _page.Locator("select[name$='__year']");
        public ILocator CardNumber => _page.Locator("input[name='41ccnumber']");
        public ILocator Cvv => _page.Locator("input[name='43cvc']");
        public ILocator CardUserName => _page.Locator("input[name='44cc_uname']");
        public ILocator CardIssuer => _page.Locator("input[name='45ccissuer']");
        public ILocator CustomerService => _page.Locator("input[name='46cccstsvc']");

        public ILocator Gender => _page.Locator("input[name='60pers_sex']");
        public ILocator SSN => _page.Locator("input[name='61pers_ssn']");
        public ILocator DrivingLicense => _page.Locator("input[name='62driv_lic']");

        public ILocator Age => _page.Locator("input[name='66pers_age']");
        public ILocator BirthPlace => _page.Locator("input[name='67birth_pl']");
        public ILocator Income => _page.Locator("input[name='68__income']");

        public ILocator CustomField => _page.Locator("input[name='71__custom']");
        public ILocator Comments => _page.Locator("input[name='72__commnt']");

        public ILocator ResetButton =>
            _page.GetByRole(AriaRole.Button,
                new() { Name = "Reset" });
    }
}