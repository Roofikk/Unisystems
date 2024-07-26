﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Unisystem.ClassroomAccount.DataContext.Entities;

public class RoomType
{
    [Key]
    [Column(TypeName = "varchar(24)")]
    public string KeyName { get; set; } = null!;
    [Column(TypeName = "varchar(64)")]
    public string DisplayName { get; set; } = null!;

    public ICollection<Classroom> Classrooms { get; set; } = [];
}