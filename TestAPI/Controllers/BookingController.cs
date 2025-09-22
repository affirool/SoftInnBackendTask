using TestAPI.DTO;
using TestAPI.Entities;
using TestAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace TestAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookingController : ControllerBase
    {
        private readonly BookingService _bookingService;

        public BookingController(BookingService bookingService) {
            _bookingService = bookingService;
        }

        [HttpGet]
        public async Task<ActionResult<List<Booking>>> GetAllBookings()
        {
            var result = await _bookingService.GetAllBookings();
            return Ok(result);
        }

        [HttpGet("{roomId}")]
        public async Task<ActionResult<List<Booking>>> GetBookingById(int roomId)
        {
            var result = await _bookingService.GetBookingById(roomId);
            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult<List<Booking>>> AddNewBooking(AddBookingDTO bookingDTO)
        {
            try
            {
                var result = await _bookingService.AddNewBooking(bookingDTO);
                return new JsonResult(result);
            }
            catch (Exception ex)
            {
                return Conflict(new { error = ex.Message });
            }
        }
    }
}
