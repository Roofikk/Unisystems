using System.ComponentModel.DataAnnotations.Schema;

namespace Unisystems.ClassroomAccount.DataContext.Entities;

public class TechEquipment
{
    public int EquipmentId { get; set; }
    [Column(TypeName = "varchar(128)")]
    public string Name { get; set; } = null!;
    [Column(TypeName = "text")]
    public string? Description { get; set; }
    public int ClassroomId { get; set; }
    public Classroom Classroom { get; set; } = null!;
}
