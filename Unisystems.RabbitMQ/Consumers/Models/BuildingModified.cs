namespace Unisystems.RabbitMQ.Consumers.Models;

public class BuildingModified : IBuilding
{
    public int BuildingId { get; set; }
    public string Name { get; set; } = null!;
    public DateTime Modified { get; set; }
}
