using Rere.Infra.Database;
using Rere.Tests.Fixtures;

namespace Rere.Extensions;

public static class HostExtensions
{
    public static IHost SeedData(this IHost host)
    {
        using var scope = host.Services.CreateScope();
        var services = scope.ServiceProvider;
        var context = services.GetRequiredService<RereDbContext>();
        DbInitialiser.Seed(context);
        return host;
    }
}