using BusinessServiceTemplate.Api.Enrichers;
using BusinessServiceTemplate.Api.Extensions;
using BusinessServiceTemplate.Api.Middlewares;
using BusinessServiceTemplate.Api.Settings;
using Microsoft.AspNetCore.OData;
//using LoggingService.Client.Extensions;
using Newtonsoft.Json;
using Serilog;
using Serilog.Events;
using Serilog.Formatting.Compact;

Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
    .Enrich.FromLogContext()
    .WriteTo.Console(new RenderedCompactJsonFormatter())
    .CreateBootstrapLogger();

var builder = WebApplication.CreateBuilder(args);

// Add Serilog Initialization
builder.Host.UseSerilog((context, services, configuration) => configuration
    .ReadFrom.Configuration(context.Configuration)
    .ReadFrom.Services(services)
    .Enrich.FromLogContext()
    .Enrich.With<EventTypeEnricher>()
    .Enrich.With<HttpRequestEnricher>()
    .Enrich.WithMachineName()
    .Enrich.WithEnvironmentName()
    .WriteTo.Console());

builder.Logging.ClearProviders();
builder.Logging.AddSerilog(Log.Logger);

var config = builder.Configuration;

JsonConvert.DefaultSettings = () => new JsonSerializerSettings
{
    Formatting = Newtonsoft.Json.Formatting.Indented,
    ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
};

// Add services to the container.
builder.Services.AddControllers().AddOData(options => options.EnableQueryFeatures());
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Add Cors enabled
builder.Services.AddCors(options =>
{
    options.AddPolicy("GLOBAL_CORS_POLICY",
        builder => builder.SetIsOriginAllowed(origin => true)
            .AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader());
});
// Base Settings Loading
builder.Services.Configure<LoggingServiceSettings>(config.GetSection("LoggingService"));
// Base Service Configuration
builder.Services.ConfigureRepositoryManager(config);
builder.Services.ConfigureAutoMapper();
builder.Services.ConfigureMediatR();
builder.Services.ConfigureAuthoization(config);
builder.Services.ConfigureSwaggerUi(config);
builder.Services.AddMemoryCache();
builder.Services.ConfigureApplicationServices();

// Setup Remote Logging Service
//builder.Services.AddApiLoggingService(config["LoggingService:BaseAddress"]);
builder.Services.AddApiLocationService(config["LocationService:BaseAddress"]);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(settings =>
    {
        settings.SwaggerEndpoint("/swagger/v1/swagger.json", "API v1.0");
        settings.OAuthClientId(config["Authorization:ClientId"]);
        settings.OAuthClientSecret(config["Authorization:ClientSecret"]);
        settings.OAuthUsePkce();
    });
}
app.UseCors("GLOBAL_CORS_POLICY");

app.UseHttpsRedirection();

app.UseMiddleware(typeof(HttpContextMiddleware));

app.UseMiddleware(typeof(ExceptionHandlingMiddleware));

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
