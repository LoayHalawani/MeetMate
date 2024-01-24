using Core.Interfaces;
using Core.Models;
using Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class RoomService : IRoomService
    {
        private readonly IUnitOfWork _unitOfWork;
        public RoomService(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }

        public async Task<Room> CreateRoom(Room newRoom)
        {
            await _unitOfWork.Room.AddAsync(newRoom);
            await _unitOfWork.CommitAsync();
            return newRoom;
        }

        public async Task DeleteRoom(Room room)
        {
            _unitOfWork.Room.Remove(room);
            await _unitOfWork.CommitAsync();
        }

        public async Task<IEnumerable<Room>> GetAllRooms()
        {
            return await _unitOfWork.Room
                .GetAllRoomsAsync();
        }

        public async Task<Room> GetRoomById(int id)
        {
            return await _unitOfWork.Room
                .GetRoomByIdAsync(id);
        }

        public async Task UpdateRoom(Room roomToBeUpdated, Room room)
        {
            roomToBeUpdated.Name = room.Name;
            roomToBeUpdated.RoomId = room.RoomId;

            await _unitOfWork.CommitAsync();
        }
    }
}
