using HotelServices.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models.Entities;

namespace HotelApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class BookingController : ControllerBase
    {
        private readonly IBookingService _bookingService;
        public BookingController(IBookingService bookingService)
        {
            _bookingService = bookingService;
        }
        // Controller actions would go here
        [HttpGet("api/AllBooking")]
        public async Task<IActionResult> GetAllBookings()
        {
            try
            {
                var userId = User.FindFirst("uid")?.Value;
                if (string.IsNullOrEmpty(userId))
                {
                    return Unauthorized("User Not Found");
                }
                var bookings = await _bookingService.GetAllBookingsAsync(userId);
                return Ok(bookings);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("api/BookingById/{id}")]
        public async Task<IActionResult> GetBookingById(int id)
        {
            try
            {
                var userId = User.FindFirst("uid")?.Value;
                if (string.IsNullOrEmpty(userId))
                {
                    return Unauthorized("User Not Found");
                }
                var booking = await _bookingService.GetBookingByIdAsync(id, userId);
                if (booking == null)
                {
                    return NotFound("Booking Not Found");
                }
                return Ok(booking);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPost("api/CreateBooking")]
        public async Task<IActionResult> CreateBooking([FromBody] Booking booking)
        {
            try
            {
                var userId = User.FindFirst("uid")?.Value;
                if (string.IsNullOrEmpty(userId))
                {
                    return Unauthorized("User Not Found");
                }
                var createdBooking = await _bookingService.CreateBookingAsync(booking, userId);
                return Ok(createdBooking);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPut("api/UpdateBooking/{id}")]
        public async Task<IActionResult> UpdateBooking(int id, [FromBody] Booking updatedBooking)
        {
            try { 
            
                var userid = User.FindFirst("uid")?.Value;
                if(string.IsNullOrEmpty(userid))
                {
                    return Unauthorized("User Not Found");
                }
                var result = await _bookingService.UpdateBookingAsync(id, updatedBooking, userid);
                if(!result)
                {
                    return NotFound("Booking Not Found");
                }
                return Ok("Booking Updated Successfully");
            }catch(Exception ex) {
                return BadRequest(ex.Message);
            }
        }
        [HttpDelete("api/DeleteBooking/{id}")]
        public async Task<IActionResult> DeleteBooking(int id)
        {
            try
            {
                var userId = User.FindFirst("uid")?.Value;
                if (string.IsNullOrEmpty(userId))
                {
                    return Unauthorized("User Not Found");
                }
                var result = await _bookingService.DeleteBookingAsync(id, userId);
                if (!result)
                {
                    return NotFound("Booking Not Found");
                }
                return Ok("Booking Deleted Successfully");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}

