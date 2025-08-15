using IdentityService.Handlers;
using IdentityService.Infrastructure.Persistence.Context;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Shared.Auth;
using System.Text;

namespace IdentityService.Bootstraper;

public static class DependencyRegistration
{

    public static void RegisterCommon(this WebApplicationBuilder builder)
    {
        builder.Services.AddControllers();
        builder.Services.AddOpenApi();
    }

    public static void RegisterMSSql(this WebApplicationBuilder builder)
    {
        builder.Services.AddDbContext<IdentityDBContext>(configure =>
        {
            object value = configure.UseSqlServer(builder.Configuration.GetConnectionString(IdentityDBContext.DefaultConnectionStringName));
        });
    }


    public static void RegisterJWT(this WebApplicationBuilder builder)
    {
        var jwtSetting = builder.Configuration.GetSection(JwtSetting.Name).Get<JwtSetting>();
        if (jwtSetting is null)
            throw new ArgumentNullException(nameof(jwtSetting), "jwtSetting not found!");

        builder.Services.AddAuthentication(Options =>
        {
            Options.DefaultSignInScheme = JwtBearerDefaults.AuthenticationScheme;
            Options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            Options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

        })
        .AddJwtBearer(configureOptions =>
        {
            configureOptions.TokenValidationParameters = new TokenValidationParameters()
            {
                ValidateIssuerSigningKey = true,
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidIssuer = jwtSetting.Issuer,
                ValidAudience = jwtSetting.Audience,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSetting.Key))
            };

        });

        builder.Services.AddAuthorization();
    }
    public static void RegisterHandlers(this WebApplicationBuilder builder)
    {
        builder.Services.AddScoped<AuthHandler>();
        builder.Services.AddScoped<JwtTokenHandler>();
    }
}
