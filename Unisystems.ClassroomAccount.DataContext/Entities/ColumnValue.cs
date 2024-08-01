using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Unisystems.ClassroomAccount.DataContext.Entities;

public abstract class ColumnValue
{
    [Key]
    public int ClassroomId { get; set; }
    [Key]
    public int ColumnId { get; set; }
    public Classroom Classroom { get; set; } = null!;
    public Column Column { get; set; } = null!;
}
