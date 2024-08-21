
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
        _factory = new WebApplicationFactory<Program>();
        Client = _factory.CreateClient();
    }

    public void Dispose()
    {
        Client.Dispose();
        _factory.Dispose();
    }
}