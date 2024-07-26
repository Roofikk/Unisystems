using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Unisystem.ClassroomAccount.DataContext;
using Unisystem.ClassroomAccount.DataContext.Entities;
using Unisystem.ClassroomAccount.WebApi.Models;

namespace Unisystem.ClassroomAccount.WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ClassroomsController : ControllerBase
{
    private readonly ClassroomContext _context;
    private readonly ILogger<ClassroomsController> _logger;

    public ClassroomsController(ClassroomContext context, ILogger<ClassroomsController> logger)
    {
        _context = context;
        _logger = logger;
    }

    // GET: api/Classrooms
    [HttpGet]
    public async Task<ActionResult<IEnumerable<ClassroomRetrieveDto>>> GetClassrooms()
    {
        return await _context.Classrooms
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
        var classroom = await _context.Classrooms.FindAsync(id);

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
        if (await _context.RoomTypes.AnyAsync(x => x.KeyName == model.RoomTypeId) == false)
        {
            return BadRequest("Room type not found");
        }

        if (await _context.Buildings.AnyAsync(x => x.BuildingId == model.BuildingId) == false)
        {
            return BadRequest("Building not found");
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

        var newClassroom = await _context.Classrooms.AddAsync(classroom);
        await _context.SaveChangesAsync();

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

        _context.Entry(model).State = EntityState.Modified;

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
            _logger.LogError(ex, $"Failed to update classroom. Id: {classroomToUpdate.ClassroomId}");
            return Conflict();
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
            RoomType = new RoomTypeDto
            {
                RoomTypeId = classroom.RoomTypeId,
                DisplayName = classroom.RoomType.DisplayName
            }
        };
    }

    private static IEnumerable<ClassroomRetrieveDto> MapClassrooms(IEnumerable<Classroom> classrooms)
    {
        return classrooms.Select(MapClassroom);
    }
}
