namespace Unisystems.RabbitMq.BuildingMessageContracts;

public class BuildingDeleted
{
    public int BuildingId { get; set; }
    public string Name { get; set; } = null!;
    public DateTime DeletedAt { get; set; }
}
