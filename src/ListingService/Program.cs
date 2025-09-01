using ListingService.Bootstraper;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.RegisterCommon();
builder.RegisterMSSql();
builder.RegisterHandlers();
builder.RegisterJWT();
builder.RegisterBroker();

builder.Services.AddHttpContextAccessor();

var app = builder.Build();

//app.UseBusinessIdMiddleware();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.Run();
