using Microsoft.EntityFrameworkCore;
using Unisystems.ClassroomAccount.DataContext;
using Unisystems.ClassroomAccount.DataContext.Entities;
using Unisystems.ClassroomAccount.WebApi.Models.RoomType;

namespace Unisystems.ClassroomAccount.WebApi.RoomTypeService;

public class RoomTypeService : IRoomTypeService
{
    private readonly ClassroomContext _context;

    public RoomTypeService(ClassroomContext context)
    {
        _context = context;
    }

    public async Task<List<RoomTypeDto>> GetRoomTypesAsync()
    {
        return await _context.RoomTypes
            .AsNoTracking()
            .Select(x => MapRoomType(x))
            .ToListAsync();
    }

    public async Task<RoomTypeDto?> GetRoomTypeAsync(string keyName)
    {
        return await _context.RoomTypes
            .AsNoTracking()
            .Select(x => new RoomTypeDto
            {
                RoomTypeId = x.KeyName,
                Name = x.DisplayName
            })
            .FirstOrDefaultAsync(x => x.RoomTypeId == keyName);
    }

    public async Task<RoomTypeDto?> AddRoomTypeAsync(RoomTypeModifyDto model)
    {
        if (await _context.RoomTypes.AnyAsync(x => x.KeyName == model.RoomTypeId))
        {
            return null;
        }

        var roomType = new RoomType
        {
            KeyName = model.RoomTypeId,
            DisplayName = model.Name
        };

        await _context.RoomTypes.AddAsync(roomType);
        return MapRoomType(roomType);
    }

    public async Task<RoomTypeDto?> UpdateRoomTypeAsync(RoomTypeModifyDto model)
    {
        var roomType = await _context.RoomTypes
            .FirstOrDefaultAsync(x => x.KeyName == model.RoomTypeId);

        if (roomType == null)
        {
            return null;
        }

        roomType.DisplayName = model.Name;
        return MapRoomType(roomType);
    }

    public async Task<bool> ExecuteDeleteRoomTypeAsync(string keyName)
    {
        var count = await _context.RoomTypes
            .Where(x => x.KeyName == keyName)
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

    private static RoomTypeDto MapRoomType(RoomType roomType)
    {
        return new RoomTypeDto
        {
            RoomTypeId = roomType.KeyName,
            Name = roomType.DisplayName
        };
    }
}
