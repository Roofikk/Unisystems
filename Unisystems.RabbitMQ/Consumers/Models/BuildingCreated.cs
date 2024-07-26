namespace Unisystems.RabbitMQ.Consumers.Models;

public class BuildingCreated : IBuilding
{
    public int BuildingId { get; set; }
    public string Name { get; set; } = null!;
    public DateTime CreatedAt { get; set; }
}
