using System.ComponentModel.DataAnnotations;

namespace Unisystems.ClassroomAccount.WebApi.Models.RoomType;

public class RoomTypeModifyDto
{
    [MaxLength(24, ErrorMessage = "RoomTypeId must be 24 characters or less")]
    public string RoomTypeId { get; set; } = null!;
    [MaxLength(64, ErrorMessage = "Name must be 64 characters or less")]
    public string Name { get; set; } = null!;
}
