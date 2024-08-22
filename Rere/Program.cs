using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Rere.Core.Repositories.Flight;
using Rere.Core.Repositories.Flight.Accessors;
using Rere.Core.Services.Flight;
using Rere.DTOs.Flight;
using Rere.Infrastructure.Database;
using Rere.Infrastructure.Extension;
using Rere.Infrastructure.Logging.FileLogger;
using Rere.Repositories.Flight;
using Rere.Repositories.Flight.Accessors;
using Rere.Services.Flight;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Add AutoMapper
builder.Services.AddAutoMapper(typeof(FlightMappingProfile));

// Repository Layer
builder.Services.AddScoped<IFlightReader, InMemoryFlightReader>();
builder.Services.AddScoped<IFlightWriter, InMemoryFlightWriter>();
builder.Services.AddScoped<IFlightRepository, FlightRepository>();

// Service Layer
builder.Services.AddScoped<IFlightService, FlightService>();

// Load customised config
builder.Services.Configure<FileLoggerOptions>(builder.Configuration.GetSection("FileLogger"));

// Add Customised Logger
builder.Services.AddSingleton<ILoggerProvider>(provider =>
{
    var options = provider.GetRequiredService<IOptions<FileLoggerOptions>>().Value;
    return new FileLoggerProvider(options.LogFileDirectory);
});

// Setup InMemory Database if only in development env
if (builder.Environment.IsDevelopment())
    builder.Services.AddDbContext<RereDbContext>(options =>
        options.UseInMemoryDatabase("Rere")
    );

var app = builder.Build();

app.UseRouting();
app.MapControllers();

if (app.Environment.IsDevelopment())
{
    // Seed Data
    app.SeedData();
    // Use swagger
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.Run();