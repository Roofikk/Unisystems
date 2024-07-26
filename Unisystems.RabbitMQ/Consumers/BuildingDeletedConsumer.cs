using MassTransit;
using Unisystems.RabbitMQ.Consumers.Models;

namespace Unisystems.RabbitMQ.Consumers;

public class BuildingDeletedConsumer : IConsumer<BuildingDeleted>
{
    public Task Consume(ConsumeContext<BuildingDeleted> context)
    {
        Console.WriteLine($"Building deleted: {context.Message.BuildingId}");
        return Task.CompletedTask;
    }
}
