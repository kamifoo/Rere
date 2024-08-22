using Microsoft.AspNetCore.Mvc.Testing;

namespace Rere.Tests.Fixtures;

public class TestRereServerFixture : IDisposable
{
    public HttpClient Client { get; private set; }
    private readonly WebApplicationFactory<Program> _factory;

    /// <summary>
    /// Initializes a new instance for testing as a fixture
    /// Creates a new instance of <see cref="WebApplicationFactory{T}"/> using the <see cref="Program"/> type,
    /// and initializes the <see cref="Client"/> property with the created client.
    /// </summary>
    public TestRereServerFixture()
    {
        _factory = new WebApplicationFactory<Program>().WithWebHostBuilder(builder =>
        {
            builder.UseSetting("http_port", "5249");
            builder.UseSetting("https_port", "7225");
            builder.UseEnvironment("Development");
        });
        Client = _factory.CreateClient(new WebApplicationFactoryClientOptions
        {
            BaseAddress = new Uri("http://localhost:5173")
        });
    }

    public void Dispose()
    {
        Client.Dispose();
        _factory.Dispose();
    }
}