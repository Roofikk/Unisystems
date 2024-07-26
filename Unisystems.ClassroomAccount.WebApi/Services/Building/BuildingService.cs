using Microsoft.EntityFrameworkCore;
using RabbitMQ.Client;
using Unisystems.ClassroomAccount.DataContext;
using Unisystems.ClassroomAccount.DataContext.Entities;

namespace Unisystems.ClassroomAccount.WebApi.BuildingService;

public class BuildingService : IBuildingService
{
    private readonly ClassroomContext _context;

    public BuildingService(ClassroomContext context)
    {
        _context = context;
    }

    public async Task<Building> CreateAsync(Building building)
    {
        await _context.Buildings.AddAsync(building);
        return building;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var count = await _context.Buildings
            .Where(x => x.BuildingId == id)
            .ExecuteDeleteAsync();

        return count > 0;
    }

    public Building Update(Building building)
    {
        _context.Buildings.Update(building);
        return building;
    }

    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }
}
