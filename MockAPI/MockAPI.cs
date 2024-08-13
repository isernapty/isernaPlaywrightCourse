using WireMock.RequestBuilders;
using WireMock.ResponseBuilders;
using WireMock.Server;

public class MockAPI
{
    private WireMockServer server;
    // Setup mock API
    public void StartServer()
    {
        // This starts a new mock server instance listening at port 9876
        server = WireMockServer.Start(9876);
    }

    public void CreateMockAuth()
    {
        server.Given(
            Request.Create().WithPath("/auth").UsingGet()
        )
        .RespondWith(
            Response.Create()
                .WithStatusCode(200)
                .WithHeader("Content-Type", "text/plain")
                .WithBody("MockToken")
        );
    }

    public void StopMockServer()
    {
        server.Stop();
    }
}