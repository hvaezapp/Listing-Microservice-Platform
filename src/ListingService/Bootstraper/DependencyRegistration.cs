using ListingService.Handlers;
using ListingService.Infrastructure.Persistence.Context;
using Shared.Auth;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using MassTransit;
using ListingService.Shared;

namespace ListingService.Bootstraper;

public static class DependencyRegistration
{

    public static void RegisterCommon(this WebApplicationBuilder builder)
    {
        builder.Services.AddControllers();
        builder.Services.AddOpenApi();
    }

    public static void RegisterMSSql(this WebApplicationBuilder builder)
    {
        builder.Services.AddDbContext<ListingDBContext>(configure =>
        {
            configure.UseSqlServer(builder.Configuration.GetConnectionString(ListingDBContext.DefaultConnectionStringName));
        });
    }


    public static void RegisterHandlers(this WebApplicationBuilder builder)
    {
        builder.Services.AddScoped<ListingHandler>();
        builder.Services.AddScoped<CurrentUserHandler>();
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


    public static void RegisterBroker(this WebApplicationBuilder builder)
    {
        builder.Services.AddMassTransit(configure =>
        {
            var brokerConfig = builder.Configuration
                                        .GetSection(BrokerOptions.SectionName)
                                        .Get<BrokerOptions>();
            if (brokerConfig is null)
                throw new ArgumentNullException(nameof(BrokerOptions), "Broker setting not found");

            configure.UsingRabbitMq((context, cfg) =>
            {
                cfg.Host(brokerConfig.Host, hostConfigure =>
                {
                    hostConfigure.Username(brokerConfig.Username);
                    hostConfigure.Password(brokerConfig.Password);
                });

                cfg.ConfigureEndpoints(context);
            });
        });
    }


}
