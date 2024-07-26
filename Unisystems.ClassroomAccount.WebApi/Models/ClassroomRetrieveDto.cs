namespace Unisystem.ClassroomAccount.WebApi.Models;

public class ClassroomRetrieveDto : ClassroomDto
{
    public int ClassroomId { get; set; }

    public RoomTypeDto RoomType { get; set; } = null!;
    public BuildingDto Building { get; set; } = null!;
}
