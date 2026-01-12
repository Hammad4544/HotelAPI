using HotelServices.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models.DTOS.Reviews;
using System.Security.Claims;

namespace HotelApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReviewController : ControllerBase
    {
        private readonly IReviewService _reviewService;

        public ReviewController(IReviewService reviewService)
        {
            _reviewService = reviewService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var reviews = await _reviewService.GetAllReviewsAsync();
            return Ok(reviews);
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> CreateOrUpdate(
            [FromBody] AddReviewsDto reviewsDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var userId = User.FindFirst("uid")?.Value ;

            if (string.IsNullOrEmpty(userId))
                return Unauthorized();

            await _reviewService.CreateReview(userId, reviewsDto);

            return Ok(new { message = "Review submitted successfully" });
        }
    }
}
