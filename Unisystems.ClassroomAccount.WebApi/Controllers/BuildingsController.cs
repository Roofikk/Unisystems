using Microsoft.AspNetCore.Mvc;
using Unisystems.ClassroomAccount.DataContext.Entities;
using Unisystems.ClassroomAccount.WebApi.BuildingService;
using Unisystems.ClassroomAccount.WebApi.Models;

namespace Unisystems.ClassroomAccount.WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class BuildingsController : ControllerBase
{
    private readonly IBuildingService _buildingService;

    public BuildingsController(IBuildingService buildingService)
    {
        _buildingService = buildingService;
    }

    // GET: api/Buildings
    [HttpGet]
    public async Task<ActionResult<IEnumerable<BuildingDto>>> GetBuildings()
    {
        return (await _buildingService.GetAllAsync())
            .Select(MapBuilding)
            .ToList();
    }

    // GET: api/Buildings/5
    [HttpGet("{id}")]
    public async Task<ActionResult<BuildingDto>> GetBuilding(int id)
    {
        if (!await _buildingService.IsExistsAsync(id))
        {
            return NotFound();
        }

        return MapBuilding(await _buildingService.GetByIdAsync(id) ?? throw new Exception("Building not found"));
    }

    private static BuildingDto MapBuilding(Building building)
    {
        return new BuildingDto
        {
            BuildingId = building.BuildingId,
            Name = building.Name
        };
    }

    private static IEnumerable<BuildingDto> MapBuildings(IEnumerable<Building> buildings)
    {
        return buildings.Select(MapBuilding);
    }
}
