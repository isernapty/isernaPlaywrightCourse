using DotNet.Testcontainers.Builders;
using FluentAssertions;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.CommonModels;

namespace Playwright_Project.Steps
{
    [Binding]
    public class TestContainersTestStepDefinitions
    {
        DotNet.Testcontainers.Containers.IContainer container;
        string guid;


        [Given(@"I run the hello world test container")]
        public async Task GivenIRunTheHelloWorldTestContainer()
        {
            // Create a new instance of a container.
            container = new ContainerBuilder()
              // Set the image for the container to "testcontainers/helloworld:1.1.0".
              .WithImage("testcontainers/helloworld:1.1.0")
              // Bind port 8080 of the container to a random port on the host.
              .WithPortBinding(8080, true)
              // Wait until the HTTP endpoint of the container is available.
              .WithWaitStrategy(Wait.ForUnixContainer().UntilHttpRequestIsSucceeded(r => r.ForPort(8080)))
              // Build the container configuration.
              .Build();

            // Start the container.
            await container.StartAsync()
              .ConfigureAwait(false);
        }

        [When(@"I make a request to the URL")]
        public async Task WhenIMakeARequestToTheURL()
        {
            // Create a new instance of HttpClient to send HTTP requests.
            var httpClient = new HttpClient();

            // Construct the request URI by specifying the scheme, hostname, assigned random host port, and the endpoint "uuid".
            var requestUri = new UriBuilder(Uri.UriSchemeHttp, container.Hostname, container.GetMappedPublicPort(8080), "uuid").Uri;

            // Send an HTTP GET request to the specified URI and retrieve the response as a string.
            guid = await httpClient.GetStringAsync(requestUri)
              .ConfigureAwait(false);
        }

        [Then(@"I obtain a valid UUID inside the response")]
        public void ThenIObtainAValidUUIDInsideTheResponse()
        {
            Guid guidResult;
            if (Guid.TryParse(guid, out guidResult))
            {
                true.Should().Be(true);
            }
            else
            {
                false.Should().Be(true);
            }
        }
    }
}
