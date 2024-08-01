using System.ComponentModel.DataAnnotations;
using Unisystems.ClassroomAccount.WebApi.Models.Columns;

namespace Unisystems.ClassroomAccount.WebApi.Models.Classroom;

public class ClassroomModifyDto : ClassroomDto
{
    [Required(ErrorMessage = "RoomTypeId is required")]
    public virtual string RoomTypeId { get; set; } = null!;
    [Required(ErrorMessage = "BuildingId is required")]
    public virtual int BuildingId { get; set; }
    public ICollection<ColumnModifyDto> Columns { get; set; } = [];
}
