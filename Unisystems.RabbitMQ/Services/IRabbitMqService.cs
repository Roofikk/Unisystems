using Unisystems.RabbitMQ.Consumers.Models;

namespace Unisystems.RabbitMQ.Services;

public interface IRabbitMqService
{
    public Task CreateBuilding(BuildingCreated buildingCreated);
    public Task UpdateBuilding(BuildingModified buildingUpdated);
    public Task DeleteBuilding(BuildingDeleted buildingDeleted);
}
