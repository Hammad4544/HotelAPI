using HotelServices.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models.DTOS.Booking;
using Models.Entities;

namespace HotelApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookingController : ControllerBase
    {
        private readonly IBookingService _bookingService;
        public BookingController(IBookingService bookingService)
        {
            _bookingService = bookingService;
        }
        // Controller actions would go here
        [HttpGet("GetAll")]
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
        [HttpGet("GetActiveBooking")]
        public async Task<IActionResult> GetActiveBooking()
        {
            var bookings = await _bookingService.GetActiveBookings();
            if(bookings == null)
            {
                return NotFound();
            }
            return Ok(bookings);
        }
        [HttpGet("GetBookingById/{id}")]
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
        [HttpPost("CreateBooking")]
        public async Task<IActionResult> CreateBooking([FromBody] CreateBookingDTO booking)
        {
            try
            {
                var userId = User.FindFirst("uid")?.Value;
                if (string.IsNullOrEmpty(userId))
                {
                    return Unauthorized("User Not Found");
                }
                if(booking == null)
                {
                    return BadRequest("Invalid booking data");
                }
                
                if(booking.CheckInDate < DateTime.Now.Date)
                {
                    return BadRequest("Check-in date cannot be in the past.");
                }
                if (booking.CheckInDate >= booking.CheckOutDate)
                {
                    return BadRequest("Check-out date must be after check-in date.");
                }
                var createdBooking = await _bookingService.CreateBookingAsync(booking, userId);
                return Ok(createdBooking);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPut("UpdateBooking/{id}")]
        public async Task<IActionResult> UpdateBooking(int id, [FromBody] UpdateBookingDto updatedBooking)
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
        [HttpDelete("DeleteBooking/{id}")]
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
            [Authorize(Roles = "Admin")]
            [HttpGet("dashboard/stats")]
            public async Task<IActionResult> GetDashboardStats()
            {
                try
                {
                    var stats = await _bookingService.GetStats();
                    return Ok(stats);
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }
            }
        [Authorize(Roles = "Admin")]
        [HttpGet("dashboard/bookings")]
        public async Task<IActionResult> GetDashboardBookings()
        {
            var data = await _bookingService.GetLatestBookings();
            return Ok(data);
        }

    }
}

