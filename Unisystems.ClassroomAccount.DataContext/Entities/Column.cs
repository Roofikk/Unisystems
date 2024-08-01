using System.ComponentModel.DataAnnotations.Schema;

namespace Unisystems.ClassroomAccount.DataContext.Entities;

public class Column
{
    public int ColumnId { get; set; }
    [Column(TypeName = "varchar(128)")]
    public string Name { get; set; } = null!;
    public string ColumnTypeId { get; set; } = null!;
    public ColumnType ColumnType { get; set; } = null!;
    public ICollection<ColumnValue> ColumnValues { get; set; } = [];
}
