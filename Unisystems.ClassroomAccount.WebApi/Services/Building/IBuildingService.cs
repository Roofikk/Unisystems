using Unisystems.ClassroomAccount.DataContext.Entities;

namespace Unisystems.ClassroomAccount.WebApi.BuildingService;

public interface IBuildingService
{
    public Task<Building> CreateAsync(Building building);
    public Building Update(Building building);
    public Task<bool> DeleteAsync(int id);
    public Task SaveChangesAsync();
}