using TestAPI.Entities;
using TestAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace TestAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoomController : ControllerBase
    {
        private readonly RoomService _roomService;

        public RoomController(RoomService roomService) {
            _roomService = roomService;
        }

        [HttpGet]
        public async Task<ActionResult<List<Room>>> GetAllRooms()
        {
            var result = await _roomService.GetAllRooms();
            return Ok(result);
        }
    }
}
