using System.ComponentModel.DataAnnotations.Schema;
using Unisystems.ClassroomAccount.DataContext.Entities;

namespace Unisystems.ClassroomAccount.DataContext;

public class InputColumnValue : ColumnValue
{
    [Column(TypeName = "varchar(128)")]
    public string Value { get; set; } = null!;
}
