using Scalar.AspNetCore;
using NLog;
using NLog.Web;
using Microsoft.OpenApi;

using SwiftParser.Data.Interfaces;
using SwiftParser.Services.Interfaces;
using SwiftParser.Services.Implementations;
using static SwiftParser.Shared.APIConstants;
using SwiftParser.Data.Repositories;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

// Configure logging
var logger = LogManager.Setup()
                       .LoadConfigurationFromFile("nlog.config")
                       .GetCurrentClassLogger();
builder.Logging.ClearProviders();
builder.Host.UseNLog();

// Configure Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(opt =>
{
    opt.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = APITitle,
        Version = APIVersion,
        Description = APIDescription,
    });

    opt.EnableAnnotations();
});

// Add services to the container
builder.Services.AddControllers();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<ISwiftRepository, SwiftRepository>();
builder.Services.AddScoped<ILogRepository, LogRepository>();
builder.Services.AddScoped<ISwiftParserService, SwiftParserService>();

WebApplication app = builder.Build();

if (app.Environment.IsDevelopment())
{
    // Configure Scalar API reference
    app.MapSwagger(APIRoutePattern);
    app.MapScalarApiReference("/", options =>
    {
        options.WithTitle(APITitle)
          .ForceDarkMode()
          .HideSearch()
          .ShowOperationId()
          .ExpandAllTags()
          .SortTagsAlphabetically()
          .SortOperationsByMethod()
          .PreserveSchemaPropertyOrder()
          .DisableAgent()
          .WithOpenApiRoutePattern(APIRoutePattern);
    });
}

app.MapControllers();

// Create database if it doesn't exist
using (var scope = app.Services.CreateScope())
{
    var uow = scope.ServiceProvider.GetRequiredService<IUnitOfWork>();
    uow.EnsureDatabaseCreated();
}

await app.RunAsync();