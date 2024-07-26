using System.ComponentModel.DataAnnotations.Schema;

namespace Unisystems.ClassroomAccount.DataContext.Entities;

public class Classroom
{
    public int ClassroomId { get; set; }
    [Column(TypeName = "varchar(255)")]
    public string Name { get; set; } = null!;
    public int Capacity { get; set; }
    public int Floor { get; set; }
    public int Number { get; set; }

    public string? RoomTypeId { get; set; }
    public int BuildingId { get; set; }

    public RoomType? RoomType { get; set; }
    public Building Building { get; set; } = null!;
}
