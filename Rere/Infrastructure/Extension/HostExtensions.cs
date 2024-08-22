using Rere.Infrastructure.Database;

namespace Rere.Infrastructure.Extension;

public static class HostExtensions
{
    public static IHost SeedData(this IHost host)
    {
        using var scope = host.Services.CreateScope();
        var services = scope.ServiceProvider;
        try
        {
            var context = services.GetRequiredService<RereDbContext>();
            DbInitialiser.Seed(context);
        }
        catch (Exception ex)
        {
            var logger = services.GetRequiredService<ILogger<Program>>();
            logger.LogError(ex, "An error occurred while seeding the database.");
        }

        return host;
    }
}