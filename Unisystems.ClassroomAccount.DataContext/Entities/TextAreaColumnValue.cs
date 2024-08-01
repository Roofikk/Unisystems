using System.ComponentModel.DataAnnotations.Schema;

namespace Unisystems.ClassroomAccount.DataContext.Entities;

public class TextAreaColumnValue : ColumnValue
{
    [Column(TypeName = "text")]
    public string Value { get; set; } = null!;
}
