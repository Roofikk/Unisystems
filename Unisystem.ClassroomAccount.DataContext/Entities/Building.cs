using System.ComponentModel.DataAnnotations.Schema;

namespace Unisystem.ClassroomAccount.DataContext.Entities;

public class Building
{
    public int BuildingId { get; set; }
    public string Name { get; set; } = null!;
    public DateTime Added { get; set; }
    public DateTime LastModified { get; set; }

    public ICollection<Classroom> Classrooms { get; set; } = [];
}
