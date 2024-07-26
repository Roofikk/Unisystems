namespace Unisystems.RabbitMQ.Consumers.Models;

public class BuildingDeleted : IBuilding
{
    public int BuildingId { get; set; }
    public string Name { get; set; } = null!;
    public DateTime DeletedAt { get; set; }
}
