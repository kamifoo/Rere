using Microsoft.EntityFrameworkCore;
using Rere.Core.Models.Flight;

namespace Rere.Infra.Database;

public class RereDbContext(
    DbContextOptions<RereDbContext> options
) : DbContext(options)
{
    public DbSet<Flight> Flights { get; set; }
}