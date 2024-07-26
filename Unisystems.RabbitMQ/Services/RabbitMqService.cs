using MassTransit;
using Unisystems.RabbitMQ.Consumers.Models;

namespace Unisystems.RabbitMQ.Services;

public class RabbitMqService : IRabbitMqService
{
    private readonly IPublishEndpoint _publishEndpoint;

    public RabbitMqService(IBus bus)
    {
        _publishEndpoint = bus;
    }

    public async Task CreateBuilding(BuildingCreated buildingCreated)
    {
        await _publishEndpoint.Publish(buildingCreated);
    }

    public async Task UpdateBuilding(BuildingModified buildingUpdated)
    {
        await _publishEndpoint.Publish(buildingUpdated);
    }

    public async Task DeleteBuilding(BuildingDeleted buildingDeleted)
    {
        await _publishEndpoint.Publish(buildingDeleted);
    }
}
