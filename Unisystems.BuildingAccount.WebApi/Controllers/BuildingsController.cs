using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
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

    // GET: api/Buildings/pagination
    [HttpGet("total-items")]
    public async Task<ActionResult<int>> GetTotalItems(int page = 1, int pageSize = 10)
    {
        return await _context.Buildings.CountAsync();
    }

    // GET: api/Buildings
    [HttpGet]
    public async Task<ActionResult<IEnumerable<BuildingRecieveDto>>> GetBuildings(
        [FromQuery] PaginationModel? pagination = null,
        [FromQuery] SortModel? sortModel = null)
    {
        var buildings = _context.Buildings.AsQueryable();

        // Сортировка
        if (sortModel != null && _context.Buildings.EntityType.FindProperty(sortModel.SortBy ?? "BuildingId") != null)
        {
            sortModel.SortBy ??= "BuildingId";
            var parameter = Expression.Parameter(typeof(Building), "item");
            var member = Expression.PropertyOrField(parameter, sortModel.SortBy);
            var keySelector = Expression.Lambda(member, parameter);

            var methodCall = Expression.Call(
                typeof(Queryable), 
                sortModel.Direction == "desc" ? "OrderByDescending" : "OrderBy",
                [typeof(Building), member.Type],
                buildings.Expression,
                Expression.Quote(keySelector));

            buildings = buildings.Provider.CreateQuery<Building>(methodCall);
        }

        // Пагинация
        if (pagination != null)
        {
            var totalItems = await _context.Buildings.CountAsync();
            pagination.CurrentPage = pagination.CurrentPage > 0 ? pagination.CurrentPage : 1;
            pagination.PageSize = pagination.PageSize > 0 ? pagination.PageSize : 10;

            if (pagination.CurrentPage > totalItems / pagination.PageSize + 1)
            {
                return Array.Empty<BuildingRecieveDto>();
            }

            buildings = buildings
                .Skip((pagination.CurrentPage - 1) * pagination.PageSize)
                .Take(pagination.PageSize);
        }

        return await buildings.Select(x => MapBuilding(x)).ToListAsync();
    }

    // GET: api/Buildings/5
    [HttpGet("{id}")]
    public async Task<ActionResult<BuildingRecieveDto>> GetBuilding(int id)
    {
        var building = await _context.Buildings.FindAsync(id);

        if (building == null)
        {
            return NotFound();
        }

        return MapBuilding(building);
    }

    // POST: api/Buildings
    [HttpPost]
    public async Task<ActionResult<BuildingRecieveDto>> PostBuilding(BuildingModifyDto building)
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
            FloorCount = buildingEntity.Entity.FloorCount,
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
                FloorCount = buildingEntity.FloorCount,
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

    private object? GetPropertyValue(Building obj, string propertyName)
    {
        if (obj == null || string.IsNullOrEmpty(propertyName))
        {
            return null;
        }

        var property = obj.GetType().GetProperty(propertyName);

        if (property == null)
        {
            return obj.BuildingId;
        }

        return property?.GetValue(obj);
    }

    private static BuildingRecieveDto MapBuilding(Building building)
    {
        return new BuildingRecieveDto
        {
            BuildingId = building.BuildingId,
            Name = building.Name,
            Address = building.Address,
            FloorCount = building.FloorCount
        };
    }

    private static List<BuildingRecieveDto> MapBuildings(IEnumerable<Building> buildings)
    {
        return buildings.Select(MapBuilding).ToList();
    }
}
