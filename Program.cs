using Scalar.AspNetCore;
using NLog;
using NLog.Web;
using Microsoft.OpenApi;
using static SwiftParser.Shared.APIConstants;

var logger = LogManager.Setup()
                       .LoadConfigurationFromFile("nlog.config")
                       .GetCurrentClassLogger();

var builder = WebApplication.CreateBuilder(args);

builder.Logging.ClearProviders();
builder.Host.UseNLog();

builder.Services.AddControllers();
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

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
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

app.Run();