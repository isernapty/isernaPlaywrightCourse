using System;
using FluentAssertions;
using System.Net;
using Microsoft.Playwright;
using Newtonsoft.Json;
using TechTalk.SpecFlow;
using System.Text;
using Gherkin;

namespace Playwright_Project.Steps
{
    [Binding]
    public class BookerApiStepDefinitions
    {

        string user;
        string pass;
        dynamic userList;
        public HttpResponseMessage _response;

        [Given(@"I extract the username and password from the JSON file")]
        public void GivenIExtractTheUsernameAndPasswordFromTheJSONFile()
        {
            using StreamReader reader = new("../../../Datasets/userList.json");
            string jsonString = reader.ReadToEnd();
            userList = JsonConvert.DeserializeObject<Object>(jsonString);
        }

        [When(@"I call the auth API endpoint")]
        public async Task WhenICallTheAuthAPIEndpoint()
        {
            var body = new StringContent(
                JsonConvert.SerializeObject(userList.adminUser),
                Encoding.UTF8,
                "application/json");
            HttpClient _client = new HttpClient();
            _response = await _client.PostAsync("https://restful-booker.herokuapp.com/auth", body);
        }

        [When(@"I call the mock auth API endpoint")]
        public async Task WhenICallTheMockAuthAPIEndpoint()
        {
            HttpClient _client = new HttpClient();
            _response = await _client.GetAsync("http://localhost:9876/auth");
        }

        [Then(@"I receive a token within the response")]
        public async Task ThenIReceiveATokenWithinTheResponse()
        {
            var body = await _response.Content.ReadAsStringAsync();
            System.Console.WriteLine(body);
            dynamic bodyJson = JsonConvert.DeserializeObject(body);
            string token = bodyJson.token;
            token.Should().HaveLength(15);
            _response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Then(@"I receive a mock token within the response")]
        public async Task ThenIReceiveAMockTokenWithinTheResponse()
        {
            var body = await _response.Content.ReadAsStringAsync();
            System.Console.WriteLine(body);
            body.Should().Be("MockToken");
            _response.StatusCode.Should().Be(HttpStatusCode.OK);
        }
    }
}
