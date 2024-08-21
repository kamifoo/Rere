using Microsoft.EntityFrameworkCore;
using Rere.Core.Repositories.Flight;
using Rere.Core.Repositories.Flight.Accessors;
using Rere.Core.Services.Flight;
using Rere.Infrastructure.Database;
using Rere.Infrastructure.Mapping;
using Rere.Repositories.Flight;
using Rere.Repositories.Flight.Accessors;
using Rere.Services.Flight;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAutoMapper(typeof(MappingProfile));
builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<RereDbContext>(options =>
    options.UseInMemoryDatabase("Rere")
);

// TODO Dependency Injections
builder.Services.AddScoped<IFlightReader, InMemoryFlightReader>();
builder.Services.AddScoped<IFlightWriter, InMemoryFlightWriter>();
builder.Services.AddScoped<IFlightRepository, FlightRepository>();
builder.Services.AddScoped<IFlightService, FlightService>();

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