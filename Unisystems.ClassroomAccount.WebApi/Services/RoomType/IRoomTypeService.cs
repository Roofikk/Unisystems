using Unisystems.ClassroomAccount.WebApi.Models.RoomType;

namespace Unisystems.ClassroomAccount.WebApi.RoomTypeService;

public interface IRoomTypeService
{
    public Task<List<RoomTypeDto>> GetRoomTypesAsync();
    public Task<RoomTypeDto?> GetRoomTypeAsync(string keyName);
    public Task<RoomTypeDto?> AddRoomTypeAsync(RoomTypeModifyDto model);
    public Task<RoomTypeDto?> UpdateRoomTypeAsync(RoomTypeModifyDto model);
    public Task<bool> ExecuteDeleteRoomTypeAsync(string keyName);
    public Task SaveChangesAsync();
}