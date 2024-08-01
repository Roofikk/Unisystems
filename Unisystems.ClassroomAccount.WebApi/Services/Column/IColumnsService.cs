using Unisystems.ClassroomAccount.DataContext.Entities;
using Unisystems.ClassroomAccount.WebApi.Models.Columns;

namespace Unisystems.ClassroomAccount.WebApi.ColumnsServices;

public interface IColumnsService
{
    public Task<List<ColumnType>> GetColumnTypesAsync();
    public Task<List<Column>> GetColumnsAsync();
    public Task<AddRangeColumnValuesResult> AddRangeColumnValuesAsync(Classroom classroom, ICollection<ColumnModifyDto> columns);
}