using System.ComponentModel.DataAnnotations.Schema;

namespace Unisystems.ClassroomAccount.DataContext.Entities;

public class Building
{
    public int BuildingId { get; set; }
    [Column(TypeName = "varchar(255)")]
    public string Name { get; set; } = null!;
    public int FloorCount { get; set; }
    public DateTimeOffset Added { get; set; }
    public DateTimeOffset LastModified { get; set; }

    public ICollection<Classroom> Classrooms { get; set; } = [];
}
