using IdentityService.Bootstraper;
using IdentityService.Endpoints;
using Scalar.AspNetCore;
using Shared.Auth;

var builder = WebApplication.CreateBuilder(args);

// option pattern for JwtSetting
builder.Services.Configure<JwtSetting>(builder.Configuration.GetSection(JwtSetting.Name));

builder.RegisterCommon();
builder.RegisterMSSql();
builder.RegisterJWT();
builder.RegisterHandlers();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}

app.UseAuthentication();
app.UseAuthorization();

app.MapAuthEndpoints();

app.Run();

