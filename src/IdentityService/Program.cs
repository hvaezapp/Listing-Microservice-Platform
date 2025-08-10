using IdentityService.Bootstraper;
using IdentityService.Handlers;
using IdentityService.Shared;

var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<JwtSetting>(builder.Configuration.GetSection(JwtSetting.Name));

builder.RegisterJWT();
builder.RegisterServices();

builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseAuthentication();
app.UseAuthorization();


app.MapPost("/token", (UserTokenHandler userTokenHandler) =>
{
    //in other service :
    // validaition
    // check username and password

    var result = userTokenHandler.GenerateToken();
    return Results.Ok(result);
});


app.Run();



public class User
{
    public string Username { get; set; }
    public string Password { get; set; }

}