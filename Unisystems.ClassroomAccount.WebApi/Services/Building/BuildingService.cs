using Microsoft.EntityFrameworkCore;
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

    public async Task<List<Building>> GetAllAsync()
    {
        return await _context.Buildings
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<Building?> GetByIdAsync(int id)
    {
        return await _context.Buildings
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.BuildingId == id);
    }

    public async Task<Building> CreateAsync(Building building)
    {
        await _context.Buildings.AddAsync(building);
        return building;
    }

    public async Task<bool> IsExistsAsync(int id)
    {
        return await _context.Buildings
            .AsNoTracking()
            .AnyAsync(x => x.BuildingId == id);
    }

    public Building Update(Building building)
    {
        _context.Buildings.Update(building);
        return building;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var count = await _context.Buildings
            .Where(x => x.BuildingId == id)
            .ExecuteDeleteAsync();

        return count > 0;
    }

    public async Task SaveChangesAsync()
    {
        if (!_context.ChangeTracker.HasChanges())
        {
            return;
        }

        await _context.SaveChangesAsync();
    }
}
