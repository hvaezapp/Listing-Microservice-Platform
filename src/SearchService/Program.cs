using SearchService.Bootstraper;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

builder.RegisterCommon();
builder.RegisterBroker();
builder.RegisterElasticSearch();
builder.RegisterAppSettings();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}

app.Run();

