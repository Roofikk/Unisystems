using MassTransit;
using Unisystems.RabbitMq.BuildingMessageContracts;

namespace Unisystems.ClassroomAccount.WebApi.Services.RabbitMq;

public class RabbitMqService : IRabbitMqService
{
    private readonly IPublishEndpoint _publishEndpoint;

    public RabbitMqService(IPublishEndpoint publishEndpoint)
    {
        _publishEndpoint = publishEndpoint;
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
