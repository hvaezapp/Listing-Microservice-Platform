using Elastic.Clients.Elasticsearch;
using Elastic.Transport;
using MassTransit;
using Microsoft.Extensions.Options;
using System.Reflection;

namespace SearchService.Bootstraper;

public static class DependencyRegistration
{
    public static void RegisterBroker(this WebApplicationBuilder builder)
    {

        builder.Services.AddMassTransit(configure =>
        {
            var brokerConfig = builder.Configuration
                                            .GetSection(BrokerOptions.SectionName)
                                            .Get<BrokerOptions>();

            ArgumentNullException.ThrowIfNull(brokerConfig, nameof(BrokerOptions));

            configure.AddConsumers(Assembly.GetExecutingAssembly());

            configure.UsingRabbitMq((context, cfg) =>
            {
                cfg.Host(brokerConfig.Host, hostConfigure =>
                {
                    hostConfigure.Username(brokerConfig.UserName);
                    hostConfigure.Password(brokerConfig.Password);
                });

                cfg.ConfigureEndpoints(context);
            });
        });

    }

    public static void RegisterElasticSearch(this WebApplicationBuilder builder)
    {
        builder.Services.AddScoped(sp =>
        {
            var elasticSettings = sp.GetRequiredService<IOptions<AppSettings>>().Value.ElasticSearchOptions;

            ArgumentNullException.ThrowIfNull(elasticSettings, nameof(ElasticSearchOptions));

            var settings = new ElasticsearchClientSettings(new Uri(elasticSettings.Host))
                                        .CertificateFingerprint(elasticSettings.Fingerprint)
                                        .Authentication(new BasicAuthentication(elasticSettings.UserName, elasticSettings.Password));

            return new ElasticsearchClient(settings);
        });
    }


    public static void RegisterAppSettings(this IHostApplicationBuilder builder)
    {
        builder.Services.Configure<AppSettings>(builder.Configuration);
    }


    public static void RegisterCommon(this WebApplicationBuilder builder)
    {
        builder.Services.AddControllers();
        builder.Services.AddOpenApi();
    }
}
