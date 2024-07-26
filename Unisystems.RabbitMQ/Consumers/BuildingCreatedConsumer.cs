using MassTransit;
using Unisystems.RabbitMQ.Consumers.Models;

namespace Unisystems.RabbitMQ.Consumers;

internal class BuildingCreatedConsumer : IConsumer<BuildingCreated>
{
    public Task Consume(ConsumeContext<BuildingCreated> context)
    {
        Console.WriteLine($"Building created: {context.Message.BuildingId}");
        return Task.CompletedTask;
    }
}
