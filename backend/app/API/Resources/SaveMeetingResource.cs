using Core.Models;

namespace API.Resources
{
    public class SaveMeetingResource
    {
        public int MeetingId { get; set; }

        public DateTime? Date { get; set; }

        public DateTime? StartTime { get; set; }

        public DateTime? EndTime { get; set; }

        public int? RoomId { get; set; }

        public int? NumberOfAttendees { get; set; }

        public bool? Status { get; set; }
    }
}
