namespace Unisystems.ClassroomAccount.WebApi.Models.Classroom;

public class ClassroomModifyDto : ClassroomDto
{
    public virtual string RoomTypeId { get; set; } = null!;
    public virtual int BuildingId { get; set; }
}
