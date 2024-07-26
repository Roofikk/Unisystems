namespace Unisystem.ClassroomAccount.WebApi.Models;

public class ClassroomModifyDto : ClassroomDto
{
    public virtual string RoomTypeId { get; set; } = null!;
    public virtual int BuildingId { get; set; }
}
