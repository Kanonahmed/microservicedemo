using Basket.API.GrpcServices;
using Basket.API.Repositories;
using DiscountGRPC.Protos;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddStackExchangeRedisCache(options =>
{
    options.Configuration = builder.Configuration.GetConnectionString("BasketDb.API");
});

builder.Services.AddGrpcClient<DiscountProtoService.DiscountProtoServiceClient>(
    options=>options.Address= new Uri(builder.Configuration.GetValue<string>("GrpcSettings:DiscountGrpcUrl")));

builder.Services.AddScoped<DiscountGrpcService>();
builder.Services.AddScoped<IBasketRepository, BasketRepository>();
var app = builder.Build();

// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
//    app.MapOpenApi();
//    app.UseSwaggerUI(options =>
//    {
//        options.SwaggerEndpoint("/openapi/v1.json", "My API v1");
//        options.RoutePrefix = "swagger"; // Access the UI at /swagger
//    });
//}

app.MapOpenApi();
app.UseSwaggerUI(options =>
{
    options.SwaggerEndpoint("/openapi/v1.json", "My API v1");
    options.RoutePrefix = "swagger"; // Access the UI at /swagger
});

app.UseAuthorization();

app.MapControllers();

app.Run();
