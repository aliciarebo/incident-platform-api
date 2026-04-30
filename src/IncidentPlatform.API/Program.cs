using System.Text;
using IncidentPlatform.API.Auth;
using IncidentPlatform.Application.Auth;
using IncidentPlatform.Application.Auth.Login;
using IncidentPlatform.Application.Incidents.AssignIncident;
using IncidentPlatform.Application.Incidents.ChangeIncidentStatus;
using IncidentPlatform.Application.Incidents.CreateIncident;
using IncidentPlatform.Application.Incidents.GetIncidentById;
using IncidentPlatform.Application.Incidents.GetIncidents;
using IncidentPlatform.Application.Incidents.GetMyIncidents;
using IncidentPlatform.Application.Incidents.GetTeamIncidents;
using IncidentPlatform.Domain.Ports;
using IncidentPlatform.Infrastructure.Auth;
using IncidentPlatform.Infrastructure.Persistence;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);
var jwtSecret = builder.Configuration["Jwt:Secret"]
    ?? throw new InvalidOperationException("JWT secret is not configured.");

var jwtIssuer = builder.Configuration["Jwt:Issuer"] ?? "IncidentPlatform";
var jwtAudience = builder.Configuration["Jwt:Audience"] ?? "IncidentPlatform";

builder.Services
    .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = jwtIssuer,
            ValidAudience = jwtAudience,
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(jwtSecret)
            )
        };
    });

builder.Services.AddAuthorization();
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
builder.Services.AddScoped<ICurrentUser, JwtCurrentUser>();
builder.Services.AddScoped<GetIncidentsHandler>();
builder.Services.AddScoped<GetIncidentByIdHandler>();
builder.Services.AddScoped<AssignIncidentHandler>();
builder.Services.AddScoped<ChangeIncidentStatusHandler>();
builder.Services.AddScoped<GetTeamIncidentsHandler>();
builder.Services.AddScoped<GetMyIncidentsHandler>();
builder.Services.AddScoped<IUserRepository, InMemoryUserRepository>();
builder.Services.AddScoped<ITokenService, JwtTokenService>();
builder.Services.AddScoped<LoginHandler>();
builder.Services.AddHttpContextAccessor();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();