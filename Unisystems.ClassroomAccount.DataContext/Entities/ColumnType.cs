using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Unisystems.ClassroomAccount.DataContext.Entities;

public class ColumnType
{
    [Key]
    [Column(TypeName = "varchar(36)")]
    public string ColumnTypeId { get; set; } = null!;
    [Column(TypeName = "varchar(64)")]
    public string DisplayName { get; set; } = null!;

    public virtual ICollection<Column> Columns { get; set; } = [];
}
