using MassTransit;
using Unisystems.RabbitMQ.Consumers.Models;

namespace Unisystems.RabbitMQ.Consumers;

public class BuildingModifiedConsumer : IConsumer<BuildingModified>
{
    public Task Consume(ConsumeContext<BuildingModified> context)
    {
        Console.WriteLine($"Building modified: {context.Message.BuildingId}");
        return Task.CompletedTask;
    }
}
