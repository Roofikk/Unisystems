using Unisystems.RabbitMq.BuildingMessageContracts;

namespace Unisystems.ClassroomAccount.WebApi.Services.RabbitMq;

public interface IRabbitMqService
{
    public Task CreateBuilding(BuildingCreated buildingCreated);
    public Task UpdateBuilding(BuildingModified buildingUpdated);
    public Task DeleteBuilding(BuildingDeleted buildingDeleted);
}
