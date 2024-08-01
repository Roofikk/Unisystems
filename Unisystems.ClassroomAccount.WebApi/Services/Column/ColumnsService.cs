using Microsoft.EntityFrameworkCore;
using Unisystems.ClassroomAccount.DataContext;
using Unisystems.ClassroomAccount.DataContext.Entities;
using Unisystems.ClassroomAccount.WebApi.Models.Columns;

namespace Unisystems.ClassroomAccount.WebApi.ColumnsServices;

public class ColumnsService : IColumnsService
{
    private readonly ClassroomContext _context;

    public ColumnsService(ClassroomContext context)
    {
        _context = context;
    }

    public async Task<List<ColumnType>> GetColumnTypesAsync()
    {
        return await _context.ColumnTypes
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<List<Column>> GetColumnsAsync()
    {
        return await _context.Columns
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<AddRangeColumnValuesResult> AddRangeColumnValuesAsync(Classroom classroom, ICollection<ColumnModifyDto> columns)
    {
        var existColumns = await _context.Columns
            .AsNoTracking()
            .ToListAsync();
        var columnTypes = await _context.ColumnTypes
            .AsNoTracking()
            .ToListAsync();

        foreach (var column in columns)
        {
            if (column.ColumnId != null)
            {
                var existColumn = existColumns.FirstOrDefault(x => x.ColumnId == column.ColumnId);

                if (existColumn == null)
                {
                    return new AddRangeColumnValuesResult
                    {
                        Success = false,
                        Message = $"Column not found: {column.ColumnId}"
                    };
                }

                switch (existColumn.ColumnType.ColumnTypeId)
                {
                    case nameof(InputColumnValue):
                        await _context.InputColumnValues.AddAsync(new InputColumnValue
                        {
                            Classroom = classroom,
                            ColumnId = existColumn.ColumnId,
                            Value = column.ColumnValue,
                        });
                        break;
                    case nameof(TextAreaColumnValue):
                        await _context.TextAreaColumnValues.AddAsync(new TextAreaColumnValue
                        {
                            Classroom = classroom,
                            ColumnId = existColumn.ColumnId,
                            Value = column.ColumnValue,
                        });
                        break;
                    case nameof(IntColumnValue):
                        if (!int.TryParse(column.ColumnValue, out var intValue))
                        {
                            return new AddRangeColumnValuesResult
                            {
                                Success = false,
                                Message = $"Column value is not an integer: {column.ColumnValue}"
                            };
                        }

                        await _context.IntColumnValues.AddAsync(new IntColumnValue
                        {
                            Classroom = classroom,
                            ColumnId = existColumn.ColumnId,
                            Value = intValue
                        });
                        break;
                    case nameof(DoubleColumnValue):
                        if (!double.TryParse(column.ColumnValue, out var doubleValue))
                        {
                            return new AddRangeColumnValuesResult
                            {
                                Success = false,
                                Message = $"Column value is not a double: {column.ColumnValue}"
                            };
                        }

                        _context.DoubleColumnValues.Add(new DoubleColumnValue
                        {
                            Classroom = classroom,
                            ColumnId = existColumn.ColumnId,
                            Value = doubleValue
                        });
                        break;
                    default:
                        return new AddRangeColumnValuesResult
                        {
                            Success = false,
                            Message = $"Unknown column type: {existColumn.ColumnType.ColumnTypeId}"
                        };
                }
            }
            else if (!string.IsNullOrEmpty(column.ColumnName) && !string.IsNullOrEmpty(column.ColumnType))
            {
                if (!columnTypes.Any(x => x.ColumnTypeId == column.ColumnType))
                {
                    return new AddRangeColumnValuesResult
                    {
                        Success = false,
                        Message = $"Column type not found: {column.ColumnType}"
                    };
                }

                var columnEntity = await _context.Columns.AddAsync(new Column
                {
                    ColumnTypeId = column.ColumnType,
                    Name = column.ColumnName,
                });

                switch (column.ColumnType)
                {
                    case nameof(InputColumnValue):
                        await _context.InputColumnValues.AddAsync(new InputColumnValue
                        {
                            Classroom = classroom,
                            Column = columnEntity.Entity,
                            Value = column.ColumnValue
                        });
                        break;
                    case nameof(TextAreaColumnValue):
                        await _context.TextAreaColumnValues.AddAsync(new TextAreaColumnValue
                        {
                            Classroom = classroom,
                            Column = columnEntity.Entity,
                            Value = column.ColumnValue
                        });
                        break;
                    case nameof(IntColumnValue):
                        if (!int.TryParse(column.ColumnValue, out var intValue))
                        {
                            return new AddRangeColumnValuesResult
                            {
                                Success = false,
                                Message = $"Column value is not an integer: {column.ColumnValue}"
                            };
                        }

                        await _context.IntColumnValues.AddAsync(new IntColumnValue
                        {
                            Classroom = classroom,
                            Column = columnEntity.Entity,
                            Value = intValue
                        });
                        break;
                    case nameof(DoubleColumnValue):
                        if (!double.TryParse(column.ColumnValue, out var doubleValue))
                        {
                            return new AddRangeColumnValuesResult
                            {
                                Success = false,
                                Message = $"Column value is not a double: {column.ColumnValue}"
                            };
                        }

                        await _context.DoubleColumnValues.AddAsync(new DoubleColumnValue
                        {
                            Classroom = classroom,
                            Column = columnEntity.Entity,
                            Value = doubleValue
                        });
                        break;
                    default:
                        return new AddRangeColumnValuesResult
                        {
                            Success = false,
                            Message = $"Unknown column type: {column.ColumnType}"
                        };
                }
            }
            else
            {
                return new AddRangeColumnValuesResult
                {
                    Success = false,
                    Message = "Column name and type are required"
                };
            }
        }

        return new AddRangeColumnValuesResult
        {
            Success = true
        };
    }
}
