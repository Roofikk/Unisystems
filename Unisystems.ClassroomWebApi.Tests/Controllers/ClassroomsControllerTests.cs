using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
using Unisystems.ClassroomAccount.DataContext;
using Unisystems.ClassroomAccount.DataContext.Entities;
using Unisystems.ClassroomAccount.WebApi.Controllers;
using Unisystems.ClassroomAccount.WebApi.Models.Classroom;

namespace Unisystems.ClassroomWebApi.Tests.Controllers;

public class ClassroomsControllerTests
{
    private readonly ClassroomContext _context;
    private readonly ClassroomsController _controller;

    public ClassroomsControllerTests()
    {
        var options = new DbContextOptionsBuilder<ClassroomContext>()
            .UseInMemoryDatabase(databaseName: $"ClassroomContext-{Guid.NewGuid()}")
            .Options;

        _context = new ClassroomContext(options);
        var logger = new Mock<ILogger<ClassroomsController>>(MockBehavior.Default);
        _controller = new ClassroomsController(_context, logger.Object);
    }

    [Fact]
    public async Task GetClassrooms_ReturnsOk()
    {
        // Arrange
        await Initialize();

        // Act
        var result = await _controller.GetClassrooms();

        // Assert
        var actionResult = Assert.IsType<ActionResult<IEnumerable<ClassroomRetrieveDto>>>(result);
        var classrooms = Assert.IsAssignableFrom<IEnumerable<ClassroomRetrieveDto>>(actionResult.Value);
        Assert.Equal(100, classrooms.Count());
    }

    [Fact]
    public async Task GetClassrooms_ReturnEmptyList()
    {
        // Act
        var result = await _controller.GetClassrooms();

        // Assert
        var actionResult = Assert.IsType<ActionResult<IEnumerable<ClassroomRetrieveDto>>>(result);
        var classrooms = Assert.IsAssignableFrom<IEnumerable<ClassroomRetrieveDto>>(actionResult.Value);
        Assert.Empty(classrooms);
    }

    [Fact]
    public async Task GetClassroom_ReturnsOk()
    {
        // Arrange
        await Initialize();

        // Act
        var result = await _controller.GetClassroom(1);

        // Assert
        var actionResult = Assert.IsType<ActionResult<ClassroomRetrieveDto>>(result);
        var classroom = Assert.IsAssignableFrom<ClassroomRetrieveDto>(actionResult.Value);
        Assert.Equal(1, classroom.Number);
    }

    [Fact]
    public async Task GetClassroom_ReturnsNotFound()
    {
        // Arrange
        await Initialize();

        // Act
        var result = await _controller.GetClassroom(987);

        // Assert
        Assert.IsType<NotFoundResult>(result.Result);
    }

    [Fact]
    public async Task PostClassroom_ReturnsOk()
    {
        // Arrange
        await Initialize();

        // Act
        var result = await _controller.PostClassroom(new ClassroomModifyDto
        {
            Name = "Test Classroom",
            Number = 1,
            Capacity = 10,
            Floor = 1,
            BuildingId = 1,
            RoomTypeId = "Room Type: 1",
        });

        // Assert
        var actionResult = Assert.IsType<CreatedAtActionResult>(result.Result);
        var classroom = Assert.IsAssignableFrom<ClassroomRetrieveDto>(actionResult.Value);
        Assert.Equal(1, classroom.Number);
    }

    [Fact]
    public async Task PutClassroom_ReturnsOk()
    {
        // Arrange
        await Initialize();

        // Act
        var result = await _controller.PutClassroom(1, new ClassroomModifyDto
        {
            Name = "Test Classroom",
            Number = 1,
            Capacity = 10,
            Floor = 1,
            BuildingId = 1,
            RoomTypeId = "Room Type: 1",
        });

        // Assert
        var actionResult = Assert.IsType<ActionResult<ClassroomRetrieveDto>>(result);
        var classroom = Assert.IsAssignableFrom<ClassroomRetrieveDto>(actionResult.Value);
        Assert.Equal(1, classroom.Number);
        Assert.Equal("Test Classroom", classroom.Name);
    }

    // Не работает в тестах из-за ExecuteDeleteAsync
    //public async Task DeleteClassroom_ReturnsOk()
    //{
    //    // Arrange
    //    await Initialize();

    //    // Act
    //    var result = await _controller.DeleteClassroom(1);

    //    // Assert
    //    Assert.IsType<OkResult>(result);
    //}

    // Не работает в тестах из-за ExecuteDeleteAsync
    //public async Task DeleteClassroom_ReturnsNotFound()
    //{
    //    // Arrange
    //    await Initialize();

    //    // Act
    //    var result = await _controller.DeleteClassroom(987);

    //    // Assert
    //    Assert.IsType<NotFoundResult>(result);
    //}

    private async Task Initialize()
    {
        await _context.Database.EnsureDeletedAsync();
        await _context.Database.EnsureCreatedAsync();

        await _context.RoomTypes.AddRangeAsync(Enumerable.Range(1, 4).Select(i => new RoomType
        {
            KeyName = $"Room Type: {i}",
            DisplayName = $"Room Type Display Name: {i}",
        }));

        await _context.Buildings.AddRangeAsync(Enumerable.Range(1, 10).Select(i => new Building
        {
            BuildingId = i,
            Name = $"Building Name: {i}",
            Added = DateTimeOffset.Now,
            LastModified = DateTimeOffset.Now,
            Classrooms = Enumerable.Range(1, 10).Select(j => new Classroom
            {
                Name = $"Classroom Name: {j}",
                Number = i * j,
                Capacity = i * j % 10,
                Floor = i * j % 4,
                RoomTypeId = $"Room Type: {j % 4 + 1}",
            }).ToList()
        }));

        await _context.SaveChangesAsync();
    }
}