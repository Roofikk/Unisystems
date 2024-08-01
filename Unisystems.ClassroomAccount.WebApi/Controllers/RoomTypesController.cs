using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Unisystems.ClassroomAccount.DataContext.Entities;
using Unisystems.ClassroomAccount.WebApi.Models.RoomType;
using Unisystems.ClassroomAccount.WebApi.RoomTypeService;

namespace Unisystems.ClassroomAccount.WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class RoomTypesController : ControllerBase
{
    private readonly ILogger<ClassroomsController> _logger;
    private readonly IRoomTypeService _roomTypeService;

    public RoomTypesController(IRoomTypeService roomTypeService, ILogger<ClassroomsController> logger)
    {
        _logger = logger;
        _roomTypeService = roomTypeService;
    }

    // GET: api/RoomTypes
    [HttpGet]
    public async Task<ActionResult<IEnumerable<RoomTypeDto>>> GetRoomTypes()
    {
        return await _roomTypeService.GetRoomTypesAsync();
    }

    // GET: api/RoomTypes/5
    [HttpGet("{id}")]
    public async Task<ActionResult<RoomTypeDto>> GetRoomType(string id)
    {
        var roomType = await _roomTypeService.GetRoomTypeAsync(id);

        if (roomType == null)
        {
            return NotFound();
        }

        return roomType;
    }

    // PUT: api/RoomTypes
    [HttpPut]
    public async Task<ActionResult<RoomTypeDto>> PutRoomType(RoomTypeModifyDto roomType)
    {
        var roomTypeDto = await _roomTypeService.UpdateRoomTypeAsync(roomType);

        if (roomTypeDto == null)
        {
            return NotFound();
        }

        try
        {
            await _roomTypeService.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException ex)
        {
            _logger.LogError(ex, "Error updating room type {RoomType}", roomType);
            return Conflict("Error updating room type");
        }

        return roomTypeDto;
    }

    // POST: api/RoomTypes
    [HttpPost]
    public async Task<ActionResult<RoomTypeDto>> PostRoomType(RoomTypeModifyDto roomType)
    {
        var roomTypeDto = await _roomTypeService.AddRoomTypeAsync(roomType);

        if (roomTypeDto == null)
        {
            return NotFound();
        }

        await _roomTypeService.SaveChangesAsync();
        return CreatedAtAction("GetRoomType", new { id = roomType.RoomTypeId }, roomTypeDto);
    }

    // DELETE: api/RoomTypes/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteRoomType(string id)
    {
        if (await _roomTypeService.ExecuteDeleteRoomTypeAsync(id))
        {
            return NoContent();
        }

        return NotFound();
    }
}
