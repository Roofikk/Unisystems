﻿using System.ComponentModel.DataAnnotations;

namespace Unisystems.ClassroomAccount.WebApi.Models.Classrooms;

public abstract class ClassroomDto
{
    [Required(ErrorMessage = "Name is required")]
    [MaxLength(255, ErrorMessage = "Name must be less than 255 characters")]
    public string Name { get; set; } = null!;
    [Range(0, int.MaxValue, ErrorMessage = "Capacity must be greater than 0")]
    public int Capacity { get; set; }
    public int Floor { get; set; }
    [Range(0, int.MaxValue, ErrorMessage = "Number must be greater than 0")]
    public int Number { get; set; }
}
