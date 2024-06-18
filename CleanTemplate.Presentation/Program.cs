using CleanTemplate.Presentation;
using CleanTemplate.Presentation.Configuration;
using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.SwaggerGen;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

if (builder.Environment.IsDevelopment())
{
    // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
    builder.Services.AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureSwaggerOptions>();
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();
}


builder.Services.AddDatabaseContext(builder.Configuration);
builder.Services.AddDI();
builder.Services.AddCustomProblemDetails();
builder.Services.AddAutoMapper();
builder.Services.AddVersioning();
builder.Services.AddApplicationInsights(builder.Configuration);
builder.Services.AddCustomHealthChecks(builder.Configuration);

var app = builder.Build();

app.UseStatusCodePages();
app.UseExceptionHandler("/error");

app.MapHealthChecks("/api/health", new HealthCheckOptions()
{
    Predicate = _ => true,
    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
});

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        var descriptions = app.DescribeApiVersions();
        foreach (var description in descriptions)
        {
            options.SwaggerEndpoint($"/swagger/{description.GroupName}/swagger.json", description.GroupName.ToUpperInvariant());
        }
    });
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
