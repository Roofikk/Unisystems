namespace Unisystems.ClassroomAccount.WebApi.Models.Columns;

public class ColumnDto
{
    public int ColumnId { get; set; }
    public string Name { get; set; } = null!;
    public string ColumnTypeId { get; set; } = null!;
}
