using IncidentPlatform.Application.Auth;
using IncidentPlatform.Application.Incidents.AssignIncident;
using IncidentPlatform.Application.Incidents.ChangeIncidentStatus;
using IncidentPlatform.Application.Incidents.CreateIncident;
using IncidentPlatform.Application.Incidents.GetIncidentById;
using IncidentPlatform.Application.Incidents.GetIncidents;
using IncidentPlatform.Domain.Ports;
using IncidentPlatform.Infrastructure.Auth;
using IncidentPlatform.Infrastructure.Persistence;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers().AddJsonOptions(options =>
 {
     options.JsonSerializerOptions.Converters.Add(
         new System.Text.Json.Serialization.JsonStringEnumConverter()
     );
 });

// Swagger (OpenAPI)
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSingleton<IIncidentRepository, InMemoryIncidentRepository>();
builder.Services.AddScoped<CreateIncidentHandler>();
builder.Services.AddScoped<ICurrentUser, FakeCurrentUser>();
builder.Services.AddScoped<GetIncidentsHandler>();
builder.Services.AddScoped<GetIncidentByIdHandler>();
builder.Services.AddScoped<AssignIncidentHandler>();
builder.Services.AddScoped<ChangeIncidentStatusHandler>();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapControllers();

app.Run();