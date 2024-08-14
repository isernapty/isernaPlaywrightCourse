using BoDi;
using Microsoft.Playwright;
using TechTalk.SpecFlow;
using WireMock.RequestBuilders;
using WireMock.ResponseBuilders;
using WireMock.Server;

namespace Playwright_Project.Hooks
{

    [Binding]
    public sealed class Hooks
    {

        MockAPI mockAPI = new MockAPI();

        private readonly IObjectContainer _objectContainer;
        private readonly ScenarioContext _scenarioContext;

        public Hooks(IObjectContainer objectContainer, ScenarioContext scenarioContext) {
            _objectContainer = objectContainer;
            _scenarioContext = scenarioContext;
        }

        [BeforeScenario]
        // Setup playwright chromium
        public async Task Setup()
        {
            // await SetupBrowser();

            mockAPI.StartServer();
            mockAPI.CreateMockAuth();
        }

        public async Task SetupBrowser()
        {
            var pw = await Playwright.CreateAsync();
            var browser = await pw.Chromium.LaunchAsync(new BrowserTypeLaunchOptions { Headless = false });
            var browserContext = await browser.NewContextAsync(new BrowserNewContextOptions { BypassCSP = true });
            var page = await browserContext.NewPageAsync();
            _objectContainer.RegisterInstanceAs(browser);
            _objectContainer.RegisterInstanceAs(page);
        }

        [AfterScenario]
        // Stop mock API
        public void StopServer()
        {
            // This stops the mock server to clean up after ourselves
            mockAPI.StopMockServer();
        }
    }
}