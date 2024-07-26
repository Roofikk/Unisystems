using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Unisystems.RabbitMQ.Consumers.Models;

public abstract class Building
{
    public int BuildingId { get; set; }
    public string Name { get; set; } = null!;
}
