using SearchService;
using SearchService.Bootstraper;

var builder = WebApplication.CreateBuilder(args);

builder.RegisterBroker();
builder.RegisterElasticSearch();

builder.Services.Configure<AppSettings>(builder.Configuration);

builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.Run();

