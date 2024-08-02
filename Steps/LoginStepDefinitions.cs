using System;
using System.Net;
using FluentAssertions;
using Microsoft.Playwright;
using Playwright_Project.PageObjects;
using TechTalk.SpecFlow;

namespace Playwright_Project.Steps
{
    [Binding]
    public class LoginStepDefinitions
    {

        private readonly IPage _page;
        private LoginPage _loginPage;

        public LoginStepDefinitions (IPage page)
        {
            _page = page;
            _loginPage = new LoginPage(_page);
        }

        [Given("I go to the main login page")]
        public async Task GivenIGoToTheMainPage()
        {
            await _loginPage.VisitLoginPage();

            // Expect a title "to contain" a substring.
            var title = await _page.TitleAsync();
            title.Should().Contain("Swag Labs");
        }

        [When("I introduce the (.*) user login credentials")]
        public async Task WhenIntroduceCredentials(string user)
        {
            await _loginPage.writeCredentials(user);
        }

        [When("I click on login button")]
        public async Task WhenAcceptTheTerms()
        {
            await _loginPage.clickLoginButton();
        }

        [Then("I should see main page")]
        public void ThenIShouldSeeTheLoginPage()
        {
            // Expects the URL to contain intro.
            var url = _page.Url;
            url.Should().Contain("inventory");
        }
        
        [Then("I should see an error about the user being locked")]
        public async Task ThenIShouldSeeALockedOutError()
        {

            string textContent = await _loginPage.getErrorText();
            textContent.Should().Be("Epic sadface: Sorry, this user has been locked out.");
        }
    }
}
