using Unisystems.ClassroomAccount.WebApi.Models.RoomType;

namespace Unisystems.ClassroomAccount.WebApi.Models.Classrooms;

public class ClassroomRetrieveDto : ClassroomDto
{
    public int ClassroomId { get; set; }

    public RoomTypeDto RoomType { get; set; } = null!;
    public BuildingDto Building { get; set; } = null!;
}
