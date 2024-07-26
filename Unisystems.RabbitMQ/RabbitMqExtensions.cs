using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Unisystems.RabbitMQ.Consumers;

namespace Unisystems.RabbitMQ;

public static class RabbitMqExtensions
{
    public static IServiceCollection AddMassTransitHostedRabbitMq(this IServiceCollection services, IConfiguration configuration)
    {
        return services.AddMassTransit(options =>
        {
            options.UsingRabbitMq((context, cfg) =>
            {
                cfg.Host(configuration["RabbitMq:Host"], "/", host =>
                {
                    host.Username(configuration["RabbitMq:Username"]!);
                    host.Password(configuration["RabbitMq:Password"]!);
                });

                cfg.ReceiveEndpoint("building-created", e =>
                {
                    e.ConfigureConsumer<BuildingCreatedConsumer>(context);
                });

                cfg.ReceiveEndpoint("building-updated", e =>
                {
                    e.ConfigureConsumer<BuildingModifiedConsumer>(context);
                });

                cfg.ReceiveEndpoint("building-deleted", e =>
                {
                    e.ConfigureConsumer<BuildingDeletedConsumer>(context);
                });
            });
        });
    }
}
