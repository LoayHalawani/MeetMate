using System;
using System.Collections.Generic;

namespace Core.Models;

public partial class Meeting
{
    public int MeetingId { get; set; }

    public DateTime? Date { get; set; }

    public DateTime? StartTime { get; set; }

    public DateTime? EndTime { get; set; }

    public int? RoomId { get; set; }

    public int? NumberOfAttendees { get; set; }

    public bool? Status { get; set; }

    public virtual Room? Room { get; set; }
}
