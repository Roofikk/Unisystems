using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Unisystems.BuildingAccount.DataContext;
using Unisystems.BuildingAccount.WebApi.Models;
using Unisystems.ClassroomAccount.WebApi.Services.RabbitMq;
using Unisystems.RabbitMq.BuildingMessageContracts;

namespace Unisystems.BuildingAccount.WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class BuildingsController : ControllerBase
{
    private readonly BuildingContext _context;
    private readonly ILogger<BuildingsController> _logger;
    private readonly IRabbitMqService _rabbitMqService;

    public BuildingsController(BuildingContext context,
        ILogger<BuildingsController> logger,
        IRabbitMqService rabbitMqService)
    {
        _context = context;
        _logger = logger;
        _rabbitMqService = rabbitMqService;
    }

    // GET: api/Buildings
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Building>>> GetBuildings()
    {
        return await _context.Buildings.ToListAsync();
    }

    // GET: api/Buildings/5
    [HttpGet("{id}")]
    public async Task<ActionResult<Building>> GetBuilding(int id)
    {
        var building = await _context.Buildings.FindAsync(id);

        if (building == null)
        {
            return NotFound();
        }

        return building;
    }

    // POST: api/Buildings
    [HttpPost]
    public async Task<ActionResult<Building>> PostBuilding(BuildingModifyDto building)
    {
        var buildingEntity = _context.Buildings.Add(new Building
        {
            Name = building.Name,
            Address = building.Address,
            FloorCount = building.FloorCount,
        });
        await _context.SaveChangesAsync();

        await _rabbitMqService.CreateBuilding(new BuildingCreated
        {
            BuildingId = buildingEntity.Entity.BuildingId,
            Name = buildingEntity.Entity.Name,
            CreatedAt = DateTime.Now,
        });

        return CreatedAtAction("GetBuilding", new { id = buildingEntity.Entity.BuildingId }, MapBuilding(buildingEntity.Entity));
    }

    // PUT: api/Buildings/5
    [HttpPut("{id}")]
    public async Task<ActionResult<BuildingRecieveDto>> PutBuilding(int id, BuildingModifyDto building)
    {
        var buildingEntity = await _context.Buildings.FindAsync(id);

        if (buildingEntity == null)
        {
            return NotFound("Building not found");
        }

        buildingEntity.Name = building.Name;
        buildingEntity.Address = building.Address;
        buildingEntity.FloorCount = building.FloorCount;

        _context.Entry(buildingEntity).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();

            await _rabbitMqService.UpdateBuilding(new BuildingModified
            {
                BuildingId = buildingEntity.BuildingId,
                Name = buildingEntity.Name,
                Modified = DateTime.Now
            });
        }
        catch (DbUpdateConcurrencyException ex)
        {
            _logger.LogError(ex, "Something went wrong while updating the building");
            return NotFound("Something went wrong while updating the building");
        }

        return MapBuilding(buildingEntity);
    }

    // DELETE: api/Buildings/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteBuilding(int id)
    {
        var building = await _context.Buildings.FindAsync(id);
        if (building == null)
        {
            return NotFound();
        }

        _context.Buildings.Remove(building);
        await _context.SaveChangesAsync();

        await _rabbitMqService.DeleteBuilding(new BuildingDeleted
        {
            BuildingId = building.BuildingId,
            Name = building.Name,
            DeletedAt = DateTime.Now
        });

        return NoContent();
    }

    private BuildingRecieveDto MapBuilding(Building building)
    {
        return new BuildingRecieveDto
        {
            BuildingId = building.BuildingId,
            Name = building.Name,
            Address = building.Address,
            FloorCount = building.FloorCount
        };
    }

    private IEnumerable<BuildingRecieveDto> MapBuildings(IEnumerable<Building> buildings)
    {
        return buildings.Select(MapBuilding);
    }
}
