using Microsoft.EntityFrameworkCore;
using Unisystems.ClassroomAccount.DataContext;
using Unisystems.ClassroomAccount.DataContext.Entities;
using Unisystems.ClassroomAccount.WebApi.Models.RoomType;
using Unisystems.ClassroomAccount.WebApi.RoomTypeService;

namespace Unisystems.ClassroomWebApi.Tests.Services;

public class RoomTypeServiceTests
{
    private readonly RoomTypeService _roomTypeService;
    private readonly ClassroomContext _context;

    public RoomTypeServiceTests()
    {
        var options = new DbContextOptionsBuilder<ClassroomContext>()
            .UseInMemoryDatabase("RoomTypes").Options;
        _context = new ClassroomContext(options);
        _roomTypeService = new RoomTypeService(_context);
    }

    [Fact]
    public async Task GetAllRoomTypesAsync_ReturnsAllRoomTypes()
    {
        // Arrange
        await Initialize();

        // Act
        var result = await _roomTypeService.GetRoomTypesAsync();

        // Assert
        Assert.Equal(10, result.Count());
    }

    [Fact]
    public async Task GetRoomType_ReturnsRoomType()
    {
        // Arrange
        await Initialize();

        // Act
        var result = await _roomTypeService.GetRoomTypeAsync("RoomType-0");

        // Assert
        Assert.NotNull(result);
        var roomType = Assert.IsType<RoomTypeDto>(result);
        Assert.Equal("RoomType-0", roomType.RoomTypeId);
        Assert.Equal("RoomType-0", roomType.Name);
    }

    [Fact]
    public async Task GetRoomType_ReturnsNull()
    {
        // Arrange
        await Initialize();

        // Act
        var result = await _roomTypeService.GetRoomTypeAsync("RoomType-132");

        // Assert
        Assert.Null(result);
    }

    [Fact]
    public async Task AddRoomType_AddsRoomType()
    {
        // Arrange
        await Initialize();

        // Act
        var result = await _roomTypeService.AddRoomTypeAsync(new RoomTypeModifyDto
        {
            RoomTypeId = "RoomType-11",
            Name = "RoomType-11",
        });

        // Assert
        Assert.NotNull(result);
        var roomType = Assert.IsType<RoomTypeDto>(result);
        Assert.Equal("RoomType-11", roomType.RoomTypeId);
        Assert.Equal("RoomType-11", roomType.Name);
    }

    [Fact]
    public async Task UpdateRoomType_UpdatesRoomType()
    {
        // Arrange
        await Initialize();

        // Act
        var result = await _roomTypeService.UpdateRoomTypeAsync(new RoomTypeModifyDto
        {
            RoomTypeId = "RoomType-0",
            Name = "RoomType-001",
        });

        // Assert
        Assert.NotNull(result);
        var roomType = Assert.IsType<RoomTypeDto>(result);
        Assert.Equal("RoomType-0", roomType.RoomTypeId);
        Assert.Equal("RoomType-001", roomType.Name);
    }

    private async Task Initialize()
    {
        await _context.Database.EnsureDeletedAsync();
        await _context.Database.EnsureCreatedAsync();

        await _context.RoomTypes.AddRangeAsync(Enumerable.Range(0, 10).Select(i => new RoomType
        {
            KeyName = $"RoomType-{i}",
            DisplayName = $"RoomType-{i}"
        }));
        await _context.SaveChangesAsync();
    }
}
