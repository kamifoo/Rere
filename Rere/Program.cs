using Microsoft.EntityFrameworkCore;
using Rere.Core.Repositories.Flight;
using Rere.Core.Repositories.Flight.Accessors;
using Rere.Core.Services.Flight;
using Rere.DTOs.Flight;
using Rere.Infrastructure.Database;
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

// Add AutoMapper
builder.Services.AddAutoMapper(typeof(FlightMappingProfile));

// Repository Layer
builder.Services.AddScoped<IFlightReader, InMemoryFlightReader>();
builder.Services.AddScoped<IFlightWriter, InMemoryFlightWriter>();
builder.Services.AddScoped<IFlightRepository, FlightRepository>();

// Service Layer
builder.Services.AddScoped<IFlightService, FlightService>();

// Customised Logger
builder.Services.AddSingleton<ILoggerProvider, FileLoggerProvider>();

var app = builder.Build();

// Use swagger
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseRouting();
app.MapControllers();

app.Run();