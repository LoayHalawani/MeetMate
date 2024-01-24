using API.Resources;
using Core.Models;
using Microsoft.AspNetCore.Mvc;
using Services.Interfaces;
using AutoMapper;
using API.Validators;
using Microsoft.AspNetCore.Authorization;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class RoomController : ControllerBase
    {
        private readonly IRoomService _roomService;
        private readonly IMapper _mapper;

        public RoomController(IRoomService roomService, IMapper mapper)
        {
            this._mapper = mapper;
            this._roomService = roomService;
        }

        [HttpGet("")]
        public async Task<ActionResult<IEnumerable<RoomResource>>> GetAllRooms()
        {
            var rooms = await _roomService.GetAllRooms();
            var roomResources = _mapper.Map<IEnumerable<Room>, IEnumerable<RoomResource>>(rooms);

            return Ok(roomResources);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<RoomResource>> GetRoomById(int id)
        {
            var room = await _roomService.GetRoomById(id);
            var roomResource = _mapper.Map<Room, RoomResource>(room);

            return Ok(roomResource);
        }

        [HttpPost("")]
        public async Task<ActionResult<RoomResource>> CreateRoom([FromBody] SaveRoomResource saveRoomResource)
        {
            var validator = new SaveRoomResourceValidator();
            var validationResult = await validator.ValidateAsync(saveRoomResource);

            if (!validationResult.IsValid)
                return BadRequest(validationResult.Errors); // this needs refining, but for demo it is ok

            var roomToCreate = _mapper.Map<SaveRoomResource, Room>(saveRoomResource);

            var newRoom = await _roomService.CreateRoom(roomToCreate);

            var room = await _roomService.GetRoomById(newRoom.RoomId);

            var roomResource = _mapper.Map<Room, RoomResource>(room);

            return Ok(roomResource);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<RoomResource>> UpdateRoom(int id, [FromBody] SaveRoomResource saveRoomResource)
        {
            var validator = new SaveRoomResourceValidator();
            var validationResult = await validator.ValidateAsync(saveRoomResource);

            var requestIsInvalid = id == 0 || !validationResult.IsValid;

            if (requestIsInvalid)
                return BadRequest(validationResult.Errors); // this needs refining, but for demo it is ok

            var roomToBeUpdate = await _roomService.GetRoomById(id);

            if (roomToBeUpdate == null)
                return NotFound();

            var room = _mapper.Map<SaveRoomResource, Room>(saveRoomResource);

            await _roomService.UpdateRoom(roomToBeUpdate, room);

            var updatedRoom = await _roomService.GetRoomById(id);
            var updatedRoomResource = _mapper.Map<Room, RoomResource>(updatedRoom);

            return Ok(updatedRoomResource);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRoom(int id)
        {
            if (id == 0)
                return BadRequest();

            var room = await _roomService.GetRoomById(id);

            if (room == null)
                return NotFound();

            await _roomService.DeleteRoom(room);

            return NoContent();
        }
    }
}
