using ListingService.Bootstraper;
using ListingService.Middleware;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddOpenApi();

builder.RegisterMSSql();
builder.RegisterHandlers();

var app = builder.Build();

app.UseMiddleware<BusinessIdMiddleware>();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.MapControllers();
app.Run();
