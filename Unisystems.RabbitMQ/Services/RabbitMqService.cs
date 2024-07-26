using MassTransit;
using Unisystems.RabbitMQ.Consumers.Models;

namespace Unisystems.RabbitMQ.Services;

public class RabbitMqService
{
    private readonly IBus _bus;

    public RabbitMqService(IBus bus)
    {
        _bus = bus;
    }

    public async Task CreateBuilding(BuildingCreated buildingCreated)
    {
        await _bus.Publish(buildingCreated);
    }

    public async Task UpdateBuilding(BuildingModified buildingUpdated)
    {
        await _bus.Publish(buildingUpdated);
    }

    public async Task DeleteBuilding(BuildingDeleted buildingDeleted)
    {
        await _bus.Publish(buildingDeleted);
    }
}
