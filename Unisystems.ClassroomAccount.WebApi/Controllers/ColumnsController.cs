using Microsoft.AspNetCore.Mvc;
using Unisystems.ClassroomAccount.WebApi.ColumnsServices;
using Unisystems.ClassroomAccount.WebApi.Models.Columns;

namespace Unisystems.ClassroomAccount.WebApi.Controllers;


[Route("api/[controller]")]
[ApiController]
public class ColumnsController : ControllerBase
{
    private readonly IColumnsService _columnService;

    public ColumnsController(IColumnsService columnService)
    {
        _columnService = columnService;
    }

    // GET: api/Columns
    [HttpGet("get-columns")]
    public async Task<ActionResult<IEnumerable<ColumnDto>>> GetColumns()
    {
        return (await _columnService.GetColumnsAsync())
            .Select(x => new ColumnDto()
            {
                ColumnId = x.ColumnId,
                ColumnTypeId = x.ColumnTypeId,
                Name = x.Name
            }).ToList();
    }

    [HttpGet("get-column-types")]
    public async Task<ActionResult<IEnumerable<ColumnTypeDto>>> GetColumnTypes()
    {
        return (await _columnService.GetColumnTypesAsync())
            .Select(x => new ColumnTypeDto()
            {
                ColumnTypeId = x.ColumnTypeId,
                Name = x.DisplayName
            }).ToList();
    }
}
