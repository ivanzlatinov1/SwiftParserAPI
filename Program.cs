using Microsoft.OpenApi.Models;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(opt =>
{
    opt.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "SwiftParser API",
        Version = "v1",
        Description = "API for parsing SWIFT messages and extracting relevant information",
    });
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapSwagger("/swagger/{documentName}/swagger.json");

    app.MapScalarApiReference("/", options =>
    {
        options.WithTitle("SwiftParser API Reference")
          .ForceDarkMode()
          .HideSearch()
          .ShowOperationId()
          .ExpandAllTags()
          .SortTagsAlphabetically()
          .SortOperationsByMethod()
          .PreserveSchemaPropertyOrder()
          .DisableAgent()
          .WithOpenApiRoutePattern("/swagger/{documentName}/swagger.json");
    });
}

app.MapControllers();

app.Run();
