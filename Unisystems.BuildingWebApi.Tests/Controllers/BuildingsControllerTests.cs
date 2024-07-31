using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
using Unisystems.BuildingAccount.DataContext;
using Unisystems.BuildingAccount.WebApi.Controllers;
using Unisystems.BuildingAccount.WebApi.Models;
using Unisystems.ClassroomAccount.WebApi.Services.RabbitMq;

namespace Unisystems.BuildingWebApi.Tests.Controllers;

public class BuildingsControllerTests
{
    private readonly BuildingContext _context;
    private readonly BuildingsController _controller;

    public BuildingsControllerTests()
    {
        var options = new DbContextOptionsBuilder<BuildingContext>()
            .UseInMemoryDatabase(databaseName: $"TestDatabase-{Guid.NewGuid}")
            .Options;

        _context = new BuildingContext(options);
        var rabbitMqService = new Mock<IRabbitMqService>(MockBehavior.Default);
        var logger = new Mock<ILogger<BuildingsController>>(MockBehavior.Default);

        _controller = new BuildingsController(_context, logger.Object, rabbitMqService.Object);
    }

    [Fact]
    public async Task GetBuildings_ReturnsAllBuildings()
    {
        // Arrange
        await InitializeDatabase();

        // Act
        var result = await _controller.GetBuildings();

        // Assert
        var actionResult = Assert.IsType<ActionResult<IEnumerable<BuildingRecieveDto>>>(result);
        var buildings = Assert.IsAssignableFrom<IEnumerable<BuildingRecieveDto>>(actionResult.Value);
        Assert.NotEmpty(buildings);
        Assert.Equal(100, buildings.Count());
        Assert.Equal("Building Name: 0", buildings.First().Name);
        Assert.Equal("Building Address: 0", buildings.First().Address);
        Assert.Equal(0, buildings.First().FloorCount);

        // Finish
        await _context.Database.EnsureDeletedAsync();
    }

    [Fact]
    public async Task GetBuildings_ReturnsEmptyList()
    {
        // Act
        var result = await _controller.GetBuildings();

        // Assert
        var actionResult = Assert.IsType<ActionResult<IEnumerable<BuildingRecieveDto>>>(result);
        var buildings = Assert.IsAssignableFrom<IEnumerable<BuildingRecieveDto>>(actionResult.Value);
        Assert.Empty(buildings);

        // Finish
        await _context.Database.EnsureDeletedAsync();
    }

    [Fact]
    public async Task GetBuilding_ReturnsBuilding()
    {
        // Arrange
        await InitializeDatabase();

        // Act
        var result = await _controller.GetBuilding(1);

        // Assert
        var actionResult = Assert.IsType<ActionResult<BuildingRecieveDto>>(result);
        var building = Assert.IsAssignableFrom<BuildingRecieveDto>(actionResult.Value);
        Assert.Equal("Building Name: 0", building.Name);
        Assert.Equal("Building Address: 0", building.Address);
        Assert.Equal(0, building.FloorCount);

        // Finish
        await _context.Database.EnsureDeletedAsync();
    }

    [Fact]
    public async Task GetBuilding_ReturnsNotFound()
    {
        // Arrange

        // Act
        var result = await _controller.GetBuilding(987);

        // Assert
        var actionResult = Assert.IsType<NotFoundResult>(result.Result);
        Assert.Equal(404, actionResult.StatusCode);

        // Finish
        await _context.Database.EnsureDeletedAsync();
    }

    [Fact]
    public async Task CreateBuilding_ShouldCreateBuilding()
    {
        // Arrange
        await InitializeDatabase();

        // Act
        await _controller.PostBuilding(new BuildingModifyDto
        {
            Name = "Building Name: 101",
            Address = "Building Address: 101",
            FloorCount = 5,
        });

        // Assert
        var result = await _controller.GetBuildings();
        var actionResult = Assert.IsType<ActionResult<IEnumerable<BuildingRecieveDto>>>(result);
        var buildings = Assert.IsAssignableFrom<IEnumerable<BuildingRecieveDto>>(actionResult.Value);
        Assert.NotEmpty(buildings);
        Assert.Equal(101, buildings.Last().BuildingId);
        Assert.Equal("Building Name: 101", buildings.Last().Name);
        Assert.Equal("Building Address: 101", buildings.Last().Address);
        Assert.Equal(5, buildings.Last().FloorCount);

        // Finish
        await _context.Database.EnsureDeletedAsync();
    }

    [Fact]
    public async Task UpdateBuilding_ShouldUpdateBuilding()
    {
        // Arrange
        await InitializeDatabase();

        // Act
        await _controller.PutBuilding(1, new BuildingModifyDto
        {
            Name = "Building Name: 101",
            Address = "Building Address: 101",
            FloorCount = 5,
        });

        // Assert
        var result = await _controller.GetBuilding(1);
        var actionResult = Assert.IsType<ActionResult<BuildingRecieveDto>>(result);
        var building = Assert.IsAssignableFrom<BuildingRecieveDto>(actionResult.Value);
        Assert.Equal("Building Name: 101", building.Name);
        Assert.Equal("Building Address: 101", building.Address);
        Assert.Equal(5, building.FloorCount);

        // Finish
        await _context.Database.EnsureDeletedAsync();
    }

    [Fact]
    public async Task DeleteBuilding_ShouldDeleteBuilding()
    {
        // Arrange
        await InitializeDatabase();

        // Act
        await _controller.DeleteBuilding(1);

        // Assert
        var result = await _controller.GetBuilding(1);
        var actionResult = Assert.IsType<NotFoundResult>(result.Result);
        Assert.Equal(404, actionResult.StatusCode);

        // Finish
        await _context.Database.EnsureDeletedAsync();
    }

    private async Task InitializeDatabase()
    {
        await _context.Database.EnsureCreatedAsync();

        await _context.Buildings.AddRangeAsync(Enumerable.Range(0, 100).Select(i => new Building()
        {
            Name = "Building Name: " + i,
            Address = "Building Address: " + i,
            FloorCount = i % 5,
        }));

        await _context.SaveChangesAsync();
    }
}