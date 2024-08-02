using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using Unisystems.ClassroomAccount.DataContext;
using Unisystems.ClassroomAccount.DataContext.Entities;
using Unisystems.ClassroomAccount.WebApi.ColumnsServices;
using Unisystems.ClassroomAccount.WebApi.Models;
using Unisystems.ClassroomAccount.WebApi.Models.Classrooms;

namespace Unisystems.ClassroomAccount.WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ClassroomsController : ControllerBase
{
    private readonly ClassroomContext _context;
    private readonly ILogger<ClassroomsController> _logger;
    private readonly IColumnsService _columnsService;

    public ClassroomsController(ClassroomContext context, ILogger<ClassroomsController> logger, IColumnsService columnsService)
    {
        _context = context;
        _logger = logger;
        _columnsService = columnsService;
    }

    [HttpGet("total-items")]
    public async Task<ActionResult<int>> CountClassrooms() => await _context.Classrooms.CountAsync();

    // GET: api/Classrooms
    [HttpGet]
    public async Task<ActionResult<IEnumerable<ClassroomRetrieveDto>>> GetClassrooms(
        [FromQuery] SortModel? sortModel = null, [FromQuery] PaginationModel? paginationModel = null)
    {
        var query = _context.Classrooms.AsQueryable();

        if (sortModel != null && _context.Classrooms.EntityType.FindProperty(sortModel.SortBy ?? "ClassroomId") != null)
        {
            sortModel.SortBy ??= "ClassroomId";
            var parameter = Expression.Parameter(typeof(Classroom), "x");
            var member = Expression.Property(parameter, sortModel.SortBy);
            var keySelector = Expression.Lambda(member, parameter);

            var methodCall = Expression.Call(
                typeof(Queryable),
                sortModel.Direction == "desc" ? "OrderByDescending" : "OrderBy",
                [typeof(Classroom), member.Type],
                query.Expression,
                Expression.Quote(keySelector));

            query = query.Provider.CreateQuery<Classroom>(methodCall);
        }

        if (paginationModel != null)
        {
            var totalItems = await _context.Classrooms.CountAsync();
            paginationModel.CurrentPage = totalItems > 0 ? paginationModel.CurrentPage : 1;
            paginationModel.PageSize = paginationModel.PageSize > 0 ? paginationModel.PageSize : 10;

            if (paginationModel.CurrentPage > totalItems / paginationModel.PageSize + 1)
            {
                return Array.Empty<ClassroomRetrieveDto>();
            }

            query = query
                .Skip((paginationModel.CurrentPage - 1) * paginationModel.PageSize)
                .Take(paginationModel.PageSize);
        }

        return await query
            .Include(x => x.Building)
            .Include(x => x.RoomType)
            .AsNoTracking()
            .Select(x => MapClassroom(x))
            .ToListAsync();
    }

    // GET: api/Classrooms/5
    [HttpGet("{id}")]
    public async Task<ActionResult<ClassroomRetrieveDto>> GetClassroom(int id)
    {
        var classroom = await _context.Classrooms
            .Include(x => x.Building)
            .Include(x => x.RoomType)
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.ClassroomId == id);

        if (classroom == null)
        {
            return NotFound();
        }

        return MapClassroom(classroom);
    }

    // POST: api/Classrooms
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPost]
    public async Task<ActionResult<ClassroomRetrieveDto>> PostClassroom(ClassroomModifyDto model)
    {
        if (!await _context.RoomTypes.AnyAsync(x => x.KeyName == model.RoomTypeId))
        {
            return BadRequest("Room type not found");
        }

        var building = await _context.Buildings
            .AsNoTracking()
            .Select(x => new { x.BuildingId, x.FloorCount })
            .FirstOrDefaultAsync(x => x.BuildingId == model.BuildingId);

        if (building == null)
        {
            return BadRequest("Building not found");
        }

        if (building.FloorCount < model.Floor)
        {
            return BadRequest("Floor is out of range");
        }

        var classroom = new Classroom
        {
            Name = model.Name,
            Capacity = model.Capacity,
            Floor = model.Floor,
            Number = model.Number,
            BuildingId = model.BuildingId,
            RoomTypeId = model.RoomTypeId
        };

        var result = await _columnsService.AddRangeColumnValuesAsync(classroom, model.Columns);

        if (!result.Success)
        {
            return BadRequest(result.Message);
        }

        var newClassroom = await _context.Classrooms.AddAsync(classroom);
        await _context.SaveChangesAsync();

        await _context.Entry(classroom).ReloadAsync();
        await _context.Entry(classroom).Reference(x => x.Building).LoadAsync();
        await _context.Entry(classroom).Reference(x => x.RoomType).LoadAsync();

        return CreatedAtAction("GetClassroom", new { id = newClassroom.Entity.ClassroomId }, MapClassroom(newClassroom.Entity));
    }

    // PUT: api/Classrooms/5
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPut("{id}")]
    public async Task<ActionResult<ClassroomRetrieveDto>> PutClassroom(int id, ClassroomModifyDto model)
    {
        var classroomToUpdate = await _context.Classrooms
            .FirstOrDefaultAsync(x => x.ClassroomId == id);

        if (classroomToUpdate == null)
        {
            return NotFound("Classroom not found");
        }

        classroomToUpdate.Name = model.Name;
        classroomToUpdate.Capacity = model.Capacity;
        classroomToUpdate.Floor = model.Floor;
        classroomToUpdate.Number = model.Number;
        classroomToUpdate.BuildingId = model.BuildingId;
        classroomToUpdate.RoomTypeId = model.RoomTypeId;

        var result = await _columnsService.AddRangeColumnValuesAsync(classroomToUpdate, model.Columns);

        if (!result.Success)
        {
            return BadRequest(result.Message);
        }

        try
        {
            await _context.SaveChangesAsync();

            await _context.Entry(classroomToUpdate).ReloadAsync();
            await _context.Entry(classroomToUpdate).Reference(x => x.Building).LoadAsync();
            await _context.Entry(classroomToUpdate).Reference(x => x.RoomType).LoadAsync();

            return MapClassroom(classroomToUpdate);
        }
        catch (DbUpdateConcurrencyException ex)
        {
            string message = $"Failed to update classroom. Id: {classroomToUpdate.ClassroomId}";
            _logger.LogError(ex, message);
            return Conflict(message);
        }
    }

    /// <summary>
    /// DELETE: api/Classrooms/5<br />
    /// Примечание: ExecuteDeleteAsync() не поддерживается в XUnit тестах. Поэтому следует изменить метод для прохождения тестов
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteClassroom(int id)
    {
        var count = await _context.Classrooms
            .Where(x => x.ClassroomId == id)
            .ExecuteDeleteAsync();

        if (count == 0)
        {
            return NotFound();
        }
        else
        {
            return NoContent();
        }
    }

    private static ClassroomRetrieveDto MapClassroom(Classroom classroom)
    {
        return new ClassroomRetrieveDto
        {
            ClassroomId = classroom.ClassroomId,
            Name = classroom.Name,
            Capacity = classroom.Capacity,
            Floor = classroom.Floor,
            Number = classroom.Number,
            Building = new BuildingDto
            {
                BuildingId = classroom.BuildingId,
                Name = classroom.Building.Name
            },
            RoomType = new Models.RoomType.RoomTypeDto
            {
                RoomTypeId = classroom.RoomTypeId,
                Name = classroom.RoomType?.DisplayName ?? null
            }
        };
    }

    private static IEnumerable<ClassroomRetrieveDto> MapClassrooms(IEnumerable<Classroom> classrooms)
    {
        return classrooms.Select(MapClassroom);
    }
}
