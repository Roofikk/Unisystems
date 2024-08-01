using System.ComponentModel.DataAnnotations;

namespace Unisystems.ClassroomAccount.WebApi.Models.Columns;

public class ColumnModifyDto
{
    public int? ColumnId { get; set; }
    public string? ColumnType { get; set; }
    [MaxLength(128, ErrorMessage = "Column name must be less than 128 characters")]
    public string? ColumnName { get; set; }
    public string ColumnValue { get; set; } = null!;
}
