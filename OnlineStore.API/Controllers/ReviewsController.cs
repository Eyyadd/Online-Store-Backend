using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OnlineStore.Application.DTOs.Order;
using OnlineStore.Application.DTOs.Review;
using OnlineStore.Application.Interfaces;
using OnlineStore.Domain.Entities;

namespace OnlineStore.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReviewsController : ControllerBase
    {
        private readonly IReviewService _reviewService;
        private readonly UserManager<User> _userManager;
        public ReviewsController(IReviewService reviewService, UserManager<User> userManager)
        {
            _reviewService = reviewService;
            _userManager = userManager;

        }

        [HttpPost("AddReveiw")]
        [Authorize(AuthenticationSchemes = "Bearer")]
        public IActionResult AddReview(int ProductVariant, AddReview review)
        {
            var Response = new GeneralResponse<Order>(false, "Sorry you can not add Review for this product");
            var AuthenticatedUser = User.FindFirstValue(ClaimTypes.NameIdentifier);
            review.UserId = AuthenticatedUser;
            review.ProductId = ProductVariant;
            var result = 0;
            result = _reviewService.AddReview(review, ProductVariant);
            if (result > 0)
            {
                Response.Success = true;
                Response.Message = "Added Review Successfully";
                return Ok(Response);
            }
            return BadRequest(Response);
        }
        [HttpGet("GetAllReviews")]
        public IActionResult GetReviews(int ProductVariantId)
        {
            var Response = new GeneralResponse<ReviewsByProductVariant>(false, "Sorry We can't display the review for this product");
            var Reviews = _reviewService.GetReviewByProductVariantId(ProductVariantId);
            if (Reviews is not null)
            {
                Response.Success = true;
                Response.Message = "Reviews Displayed Successfully";
                Response.Data = Reviews;
                return Ok(Response);
            }
            return BadRequest(Response);
        }

        [HttpPost("UpdateReview")]
        [Authorize(AuthenticationSchemes = "Bearer")]
        public IActionResult UpdateReview(AddReview review)
        {
            var Response = new GeneralResponse<AddReview>(false, "Sorry We can't update the review for this product");
            var AuthenticatedUser = User.FindFirstValue(ClaimTypes.NameIdentifier);
            review.UserId = AuthenticatedUser;
            var updatedReview=_reviewService.UpdateReview(review);
            if (updatedReview is not null)
            {
                Response.Success=true;
                Response.Message = "Updated Review Successfully";
                Response.Data = updatedReview;
                return Ok(Response);
            }
            return BadRequest(Response);
        }

        [HttpDelete("DeleteReview")]
        [Authorize(AuthenticationSchemes = "Bearer")]
        public IActionResult DeleteReview(AddReview review)
        {
            var Response = new GeneralResponse<AddReview>(false, "Sorry We can't delete the review for this product");
            var AuthenticatedUser = User.FindFirstValue(ClaimTypes.NameIdentifier);
            review.UserId = AuthenticatedUser;
            var updatedReview = _reviewService.DeleteReview(review.Id, review.UserId);
            if (updatedReview is not null)
            {
                Response.Success = true;
                Response.Message = "delete Review Successfully";
                Response.Data = updatedReview;
                return Ok(Response);
            }
            return BadRequest(Response);
        }
    }
}
