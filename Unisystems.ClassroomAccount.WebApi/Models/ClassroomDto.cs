using System.ComponentModel.DataAnnotations;

namespace Unisystem.ClassroomAccount.WebApi.Models;

public abstract class ClassroomDto
{
    public string Name { get; set; } = null!;
    [MinLength(1, ErrorMessage = "Capacity must be greater than 0")]
    public int Capacity { get; set; }
    public int Floor { get; set; }
    [MinLength(0, ErrorMessage = "Number must be greater or equal than 0")]
    public int Number { get; set; }
}
