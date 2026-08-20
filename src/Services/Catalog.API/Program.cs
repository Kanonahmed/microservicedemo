using Catalog.API.HostingService;
using Catalog.API.Interfaces.Manager;
using Catalog.API.Manager;
using Microsoft.AspNetCore.Http.Metadata;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.


// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
//builder.Services.AddSwaggerGen();
builder.Services.AddOpenApi();
builder.Services.AddHostedService<AppHostedService>();
builder.Services.AddScoped<IProductManager, ProductManager>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();

    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/openapi/v1.json", "My API v1");
        options.RoutePrefix = "swagger"; // Access the UI at /swagger
    });
}

app.UseAuthorization();

app.MapControllers();

app.Run();
