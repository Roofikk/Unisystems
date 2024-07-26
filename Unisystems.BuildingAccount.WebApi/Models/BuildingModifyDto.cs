namespace Unisystems.BuildingAccount.WebApi.Models;

public class BuildingModifyDto
{
    public string Name { get; set; } = null!;
    public string Address { get; set; } = null!;
    public int FloorCount { get; set; }
}
