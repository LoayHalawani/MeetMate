using Core.Models;

namespace API.Resources
{
    public class SaveRoomResource
    {
        public int RoomId { get; set; }

        public string? Name { get; set; }

        public string? Location { get; set; }

        public int? Capacity { get; set; }

        public string? Description { get; set; }

        public int? CompanyId { get; set; }
    }
}
