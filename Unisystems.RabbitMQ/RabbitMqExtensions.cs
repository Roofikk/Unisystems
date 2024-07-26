using MassTransit;
using MassTransit.Configuration;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Unisystems.RabbitMQ.Models;
namespace Unisystems.RabbitMQ;

public static class RabbitMqExtensions
{
    public static IServiceCollection AddMassTransitHostedRabbitMq(this IServiceCollection services, IConfiguration configuration)
    {
        if (configuration["RabbitMq:Host"] == null || configuration["RabbitMq:UserName"] == null || configuration["RabbitMq:Password"] == null)
        {
            throw new Exception("RabbitMq configuration not found. Need RabbitMq:Host, RabbitMq:UserName and RabbitMq:Password fields.");
        }

        return services.AddMassTransit(options =>
        {
            options.SetKebabCaseEndpointNameFormatter();
            options.UsingRabbitMq((context, cfg) =>
            {
                cfg.Host(configuration["RabbitMq:Host"], host =>
                {
                    host.Username(configuration["RabbitMq:UserName"]!);
                    host.Password(configuration["RabbitMq:Password"]!);
                });

                cfg.ConfigureEndpoints(context);
                cfg.UseRawJsonSerializer();
            });
        });
    }
}
