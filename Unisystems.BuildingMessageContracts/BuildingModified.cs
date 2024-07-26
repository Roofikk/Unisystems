namespace Unisystems.RabbitMq.BuildingMessageContracts;

public class BuildingModified
{
    public int BuildingId { get; set; }
    public string Name { get; set; } = null!;
    public DateTime Modified { get; set; }
}
