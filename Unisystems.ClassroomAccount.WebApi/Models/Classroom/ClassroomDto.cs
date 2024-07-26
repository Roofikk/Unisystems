using System.ComponentModel.DataAnnotations;

namespace Unisystems.ClassroomAccount.WebApi.Models.Classroom;

public abstract class ClassroomDto
{
    public string Name { get; set; } = null!;
    [Range(0, int.MaxValue, ErrorMessage = "Capacity must be greater than 0")]
    public int Capacity { get; set; }
    public int Floor { get; set; }
    [Range(0, int.MaxValue, ErrorMessage = "Number must be greater than 0")]
    public int Number { get; set; }
}
