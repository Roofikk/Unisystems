using Unisystems.ClassroomAccount.DataContext.Entities;
using Unisystems.RabbitMq.BuildingMessageContracts;

namespace Unisystems.ClassroomAccount.WebApi.Mapper;

public static class BuildingMessageMapper
{
    public static Building ToBuildingEntity(this BuildingCreated buildingMessage)
    {
        return new Building
        {
            BuildingId = buildingMessage.BuildingId,
            Name = buildingMessage.Name,
            FloorCount = buildingMessage.FloorCount,
            Added = buildingMessage.CreatedAt.ToUniversalTime(),
            LastModified = buildingMessage.CreatedAt.ToUniversalTime(),
        };
    }

    public static Building ToBuildingEntity(this BuildingModified buildingMessage)
    {
        return new Building
        {
            BuildingId = buildingMessage.BuildingId,
            Name = buildingMessage.Name,
            FloorCount = buildingMessage.FloorCount,
            LastModified = buildingMessage.Modified.ToUniversalTime(),
        };
    }
}
