using YouthOpportunities.Application;
using YouthOpportunities.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddApplication();
builder.Services.AddInfrastructure(builder.Configuration);

var app = builder.Build();

app.UseHttpsRedirection();

app.MapGet("/health", () => Results.Ok(new { status = "Healthy" }))
    .WithName("HealthCheck");

app.MapControllers();

app.Run();
