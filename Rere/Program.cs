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

builder.Services.AddDbContext<RereDbContext>(options =>
    options.UseInMemoryDatabase("Rere")
);

// Add customised config
builder.Services.Configure<FileLoggerOptions>(builder.Configuration.GetSection("FileLogger"));

// Add AutoMapper
builder.Services.AddAutoMapper(typeof(FlightMappingProfile));

// Repository Layer
builder.Services.AddScoped<IFlightReader, InMemoryFlightReader>();
builder.Services.AddScoped<IFlightWriter, InMemoryFlightWriter>();
builder.Services.AddScoped<IFlightRepository, FlightRepository>();

// Service Layer
builder.Services.AddScoped<IFlightService, FlightService>();

// Customised Logger
builder.Services.AddSingleton<ILoggerProvider>(provider =>
{
    var options = provider.GetRequiredService<IOptions<FileLoggerOptions>>().Value;
    return new FileLoggerProvider(options.LogFileDirectory);
});

var app = builder.Build();

// Use swagger
if (app.Environment.IsDevelopment())
{
    // Seed Data
    app.SeedData();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseRouting();
app.MapControllers();

app.Run();