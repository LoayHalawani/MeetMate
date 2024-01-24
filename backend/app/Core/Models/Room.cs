using System;
using System.Collections.Generic;

namespace Core.Models;

public partial class Room
{
    public int RoomId { get; set; }

    public string? Name { get; set; }

    public string? Location { get; set; }

    public int? Capacity { get; set; }

    public string? Description { get; set; }

    public int? CompanyId { get; set; }

    public virtual Company? Company { get; set; }

    public virtual ICollection<Meeting> Meetings { get; set; } = new List<Meeting>();
}
