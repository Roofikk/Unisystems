namespace Unisystems.RabbitMq.BuildingMessageContracts;

public class BuildingCreated
{
    public int BuildingId { get; set; }
    public string Name { get; set; } = null!;
    public int FloorCount { get; set; }
    public DateTime CreatedAt { get; set; }
}
