using System.ComponentModel.DataAnnotations;

namespace Unisystems.BuildingAccount.WebApi.Models;

public class BuildingModifyDto
{
    [Required(ErrorMessage = "Building Name is required")]
    public string Name { get; set; } = null!;
    [Required(ErrorMessage = "Address is required")]
    public string Address { get; set; } = null!;
    [Required(ErrorMessage = "Floor Count is required")]
    [Range(1, int.MaxValue, ErrorMessage = "Floor Count must be greater than 0")]
    public int FloorCount { get; set; }
}
