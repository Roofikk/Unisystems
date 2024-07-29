using System.ComponentModel.DataAnnotations;

namespace Unisystems.ClassroomAccount.WebApi.Models.Classroom;

public class ClassroomModifyDto : ClassroomDto
{
    [Required(ErrorMessage = "RoomTypeId is required")]
    public virtual string RoomTypeId { get; set; } = null!;
    [Required(ErrorMessage = "BuildingId is required")]
    public virtual int BuildingId { get; set; }
}
