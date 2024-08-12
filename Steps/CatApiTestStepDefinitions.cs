using System.Dynamic;
using System.Net;
using FluentAssertions;
using TechTalk.SpecFlow;

namespace Playwright_Project.Steps
{
    [Binding]
    public class CatApiTestStepDefinitions
    {

        dynamic _queryVars = new ExpandoObject();
        public HttpResponseMessage _response;


        [Given("I prepare the query vars for the request")]
        public void GivenIPrepareTheQueryVarsForTheRequest()
        {
            _queryVars.max_length = 20;
        }

        [When("I send the GET request to the API")]
        public async Task WhenISendTheGETRequestToTheAPI()
        {
            HttpClient _client = new HttpClient();
            _response = await _client.GetAsync("https://catfact.ninja/fact");
        }

        [Then("I receive the response with a status 200")]
        public void ThenIReceiveTheResponseWithAStatus()
        {
            _response.StatusCode.Should().Be(HttpStatusCode.OK);
        }
    }
}
