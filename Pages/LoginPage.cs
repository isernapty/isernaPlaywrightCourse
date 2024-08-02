using Microsoft.Playwright;
namespace Playwright_Project.PageObjects
{
    public class LoginPage
    {
        private readonly IPage _page;
        private readonly string userBoxLocator = "#user-name";
        private readonly string passBoxLocator = "#password"; 
        private readonly string loginButtonLocator = "#login-button"; 
        private readonly string errorLocator = "\".error-message-container > h3:nth-child(1)\""; 

        public class Credentials
        {
            public string User;
            public string Pass;
            public Credentials(string user, string pass)
            {
                User = user;
                Pass = pass;
            }
        }

        Dictionary<string, Credentials> creds = new Dictionary<string, Credentials>()
        {
            { "standard_user", new Credentials("standard_user", "secret_sauce") },
            { "locked_out_user", new Credentials("locked_out_user", "secret_sauce") },
        };

        public LoginPage(IPage page)
        {
            _page = page;
        }

        public async Task VisitLoginPage()
        {
            await _page.GotoAsync("https://www.saucedemo.com/");
        }

        public async Task writeCredentials(string user)
        {
            var userBox = _page.Locator(userBoxLocator);
            var passBox = _page.Locator(passBoxLocator);
            await userBox.TypeAsync(creds[user].User);
            await passBox.TypeAsync(creds[user].Pass);
        } 
        public async Task clickLoginButton()
        {
            var loginButton = _page.Locator(loginButtonLocator);
            // Click the login button
            await loginButton.ClickAsync(new() { Force = true });
        }  
        public async Task<string> getErrorText()
        {
            var errorMessage = _page.Locator(errorLocator);
            var textContent = await errorMessage.TextContentAsync();
            return textContent;
        }
    }
}