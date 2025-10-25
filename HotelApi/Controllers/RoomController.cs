using HotelServices.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography;

namespace HotelApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoomController : ControllerBase
    {
        public readonly IRoomService _roomService;
        public RoomController(IRoomService roomService) {
        
             _roomService = roomService;
        }
        [HttpGet("GetAll")]
        public IActionResult GetAll()
        {
            var rooms =  _roomService.GetAllRooms();
            return Ok(rooms);
        }
        [HttpGet("GetRoomById/{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var room = await _roomService.GetRoomById(id);
            if (room == null)
            {
                return NotFound();
            }
            return Ok(room);
        }
        [HttpPost("CreateRoom")]
        public async Task<IActionResult> Create([FromBody] Models.Entities.Room room)
        {
            var createdRoom = await _roomService.CreateRoom(room);
            return CreatedAtAction(nameof(GetById), new { id = createdRoom.RoomId }, createdRoom);
        }
        [HttpPut("UpdateRoom/{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] Models.Entities.Room updatedRoom)
        {
            var result = await _roomService.UpdateRoom(id, updatedRoom);
            if (!result)
            {
                return NotFound();
            }
            return Ok(updatedRoom);
        }
        [HttpDelete("DeleteRoom/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var delteRoom = await _roomService.GetRoomById(id);
            var result = await _roomService.DeleteRoom(id);
            if (!result)
            {
                return NotFound();
            }
            return Ok(delteRoom);
        }
    }
}
