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
            options.AddConsumer<BuildingCreatedConsumer>();
            options.AddConsumer<BuildingModifiedConsumer>();
            options.AddConsumer<BuildingDeletedConsumer>();

            options.UsingRabbitMq((context, cfg) =>
            {
                cfg.Host("amqp://localhost:5672", host =>
                {
                    host.Username("guest");
                    host.Password("guest");
                });

                cfg.ReceiveEndpoint("building-created-queue", e =>
                {
                    e.ConfigureConsumer<BuildingCreatedConsumer>(context);
                });

                cfg.ReceiveEndpoint("building-modified-queue", e =>
                {
                    e.ConfigureConsumer<BuildingModifiedConsumer>(context);
                });

                cfg.ReceiveEndpoint("building-deleted-queue", e =>
                {
                    e.ConfigureConsumer<BuildingDeletedConsumer>(context);
                });

                cfg.UseRawJsonSerializer();
                cfg.ConfigureEndpoints(context);
            });
        });
    }
}
