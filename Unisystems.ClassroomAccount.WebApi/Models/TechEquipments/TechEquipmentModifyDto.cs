using Unisystems.ClassroomAccount.DataContext.Entities;

namespace Unisystems.ClassroomAccount.WebApi.Models.TechEquipments;

public class TechEquipmentModifyDto
{
    public string Name { get; set; } = null!;
    public string? Description { get; set; }
    public int? ClassroomId { get; }
    public Classroom? Classroom { get; }
}
