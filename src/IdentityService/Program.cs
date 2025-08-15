using IdentityService.Bootstraper;
using IdentityService.Handlers;
using Shared.Auth;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

// option pattern for JwtSetting
builder.Services.Configure<JwtSetting>(builder.Configuration.GetSection(JwtSetting.Name));

builder.RegisterJWT();
builder.RegisterHandlers();

builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}

app.UseAuthentication();
app.UseAuthorization();

app.MapPost("/token", (UserTokenHandler userTokenHandler) =>
{
    //in other service :
        // validaition
        // check username and password

    return Results.Ok(userTokenHandler.GenerateToken());
});


app.Run();

