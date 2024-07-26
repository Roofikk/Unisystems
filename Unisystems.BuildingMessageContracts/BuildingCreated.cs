namespace Unisystems.RabbitMq.BuildingMessageContracts;

public class BuildingCreated
{
    public int BuildingId { get; set; }
    public string Name { get; set; } = null!;
    public DateTime CreatedAt { get; set; }
}
