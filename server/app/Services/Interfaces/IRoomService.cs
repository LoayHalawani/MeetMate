using Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Interfaces
{
    public interface IRoomService
    {
        Task<IEnumerable<Room>> GetAllRooms();
        Task<Room> GetRoomById(int id);
        Task<Room> CreateRoom(Room newRoom);
        Task UpdateRoom(Room roomToBeUpdated, Room room);
        Task DeleteRoom(Room room);
    }
}
