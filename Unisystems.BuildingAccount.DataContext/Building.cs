using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Unisystems.BuildingAccount.DataContext;

public class Building
{
    [Key]
    public int BuildingId { get; set; }
    [Column(TypeName = "varchar(255)")]
    public string Name { get; set; } = null!;
    [Column(TypeName = "varchar(255)")]
    public string Address { get; set; } = null!;
    public int FloorCount { get; set; }
}
