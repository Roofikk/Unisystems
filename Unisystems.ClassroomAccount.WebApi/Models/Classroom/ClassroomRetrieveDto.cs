using Unisystems.ClassroomAccount.WebApi.Models.RoomType;

namespace Unisystems.ClassroomAccount.WebApi.Models.Classroom;

public class ClassroomRetrieveDto : ClassroomDto
{
    public int ClassroomId { get; set; }

    public RoomTypeDto RoomType { get; set; } = null!;
    public BuildingDto Building { get; set; } = null!;
}
