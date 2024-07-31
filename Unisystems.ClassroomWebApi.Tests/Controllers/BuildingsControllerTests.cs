using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unisystems.ClassroomAccount.DataContext.Entities;
using Unisystems.ClassroomAccount.WebApi.BuildingService;
using Unisystems.ClassroomAccount.WebApi.Controllers;
using Unisystems.ClassroomAccount.WebApi.Models;

namespace Unisystems.ClassroomWebApi.Tests.Controllers;

public class BuildingsControllerTests
{
    private readonly Mock<IBuildingService> _buildingService;
    private readonly BuildingsController _buildingsController;

    public BuildingsControllerTests()
    {
        _buildingService = new Mock<IBuildingService>();
        _buildingsController = new BuildingsController(_buildingService.Object);
    }

    [Fact]
    public async Task GetBuildings_ReturnsAllBuildings()
    {
        // Arrange
        _buildingService.Setup(x => x.GetAllAsync()).ReturnsAsync(GetBuildings());

        // Act
        var result = await _buildingsController.GetBuildings();

        // Assert
        var actionResult = Assert.IsType<ActionResult<IEnumerable<BuildingDto>>>(result);
        var buildings = Assert.IsAssignableFrom<IEnumerable<BuildingDto>>(actionResult.Value);
        Assert.Equal(10, buildings.Count());
    }

    [Fact]
    public async Task GetBuildings_ReturnsEmptyList()
    {
        // Arrange
        _buildingService.Setup(x => x.GetAllAsync()).ReturnsAsync([]);

        // Act
        var result = await _buildingsController.GetBuildings();

        // Assert
        var actionResult = Assert.IsType<ActionResult<IEnumerable<BuildingDto>>>(result);
        var buildings = Assert.IsAssignableFrom<IEnumerable<BuildingDto>>(actionResult.Value);
        Assert.Empty(buildings);
    }

    [Fact]
    public async Task GetBuilding_ReturnsBuilding()
    {
        // Arrange
        _buildingService.Setup(x => x.GetByIdAsync(1)).ReturnsAsync(new Building
        {
            BuildingId = 1,
            Name = "Building Name: 1",
            Added = DateTimeOffset.Now,
            LastModified = DateTimeOffset.Now,
        });
        _buildingService.Setup(x => x.IsExistsAsync(1)).ReturnsAsync(true);

        // Act
        var result = await _buildingsController.GetBuilding(1);

        // Assert
        var actionResult = Assert.IsType<ActionResult<BuildingDto>>(result);
        var building = Assert.IsAssignableFrom<BuildingDto>(actionResult.Value);
        Assert.Equal(1, building.BuildingId);
    }

    [Fact]
    public async Task GetBuilding_ReturnsNotFound()
    {
        // Arrange
        _buildingService.Setup(x => x.IsExistsAsync(1)).ReturnsAsync(false);

        // Act
        var result = await _buildingsController.GetBuilding(1);

        // Assert
        Assert.IsType<NotFoundResult>(result.Result);
    }

    private List<Building> GetBuildings()
    {
        return Enumerable.Range(1, 10).Select(x => new Building
        {
            BuildingId = x,
            Name = $"Building Name: {x}",
            Added = DateTimeOffset.Now,
            LastModified = DateTimeOffset.Now,
        }).ToList();
    }
}
