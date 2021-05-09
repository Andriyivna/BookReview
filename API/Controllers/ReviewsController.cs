using API.Dtos;
using API.Entities;
using API.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReviewsController : ControllerBase
    {
        private readonly IReviewsRepository _repo;
        private readonly UserManager<User> _userManager;
        private readonly IMapper _mapper;

        public ReviewsController(IReviewsRepository repo, UserManager<User> userManager, IMapper mapper)
        {
            _repo = repo;
            _userManager = userManager;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<ReviewReturnDto>>> GetReviews()
        {
            var reviewsFromDb = await _repo.GetReviewsAsync();

            var data = _mapper.Map<IReadOnlyList<ReviewReturnDto>>(reviewsFromDb);

            return Ok(data);
        }

        [HttpGet("book/{bookId}")]
        public async Task<ActionResult<IReadOnlyList<ReviewReturnDto>>> GetReviewsForBook(int bookId)
        {
            var reviewsFromDb = await _repo.GetReviewsByBookIdAsync(bookId);

            var data = _mapper.Map<IReadOnlyList<ReviewReturnDto>>(reviewsFromDb);

            return Ok(data);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ReviewReturnDto>> GetReviewById(int id)
        {
            var reviewFromDb = await _repo.GetReviewByIdAsync(id);

            var data = _mapper.Map<ReviewReturnDto>(reviewFromDb);

            return Ok(data);
        }

        [Authorize]
        [HttpPost]
        public async Task<ActionResult<ReviewReturnDto>> AddReview(ReviewAddDto reviewToAdd)
        {
            var email = User.FindFirstValue(ClaimTypes.Email);

            var user = await _userManager.FindByEmailAsync(email);

            var review = _mapper.Map<Review>(reviewToAdd);

            review.AddedAt = DateTime.Now;
            review.UserId = user.Id;

            var addedReview = await _repo.AddReviewAsync(review);

            var reviewToReturn = _mapper.Map<ReviewReturnDto>(addedReview);

            return CreatedAtAction(nameof(GetReviewById), new { id = reviewToReturn.Id }, reviewToReturn);
        }

        [Authorize]
        [HttpPut]
        public async Task<ActionResult<ReviewReturnDto>> UpdateAuthor(ReviewUpdateDto reviewToUpdate)
        {
            var review = await _repo.GetReviewByIdAsync(reviewToUpdate.Id);

            if (review == null) return NotFound();

            var email = User.FindFirstValue(ClaimTypes.Email);

            var user = await _userManager.FindByEmailAsync(email);

            if (user.Id != review.UserId) return Unauthorized();

            review.GivenRate = reviewToUpdate.GivenRate;
            review.Content = reviewToUpdate.Content;

            await _repo.UpdateReviewAsync(review);

            return NoContent();
        }

        [Authorize]
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteReview(int id)
        {
            var review = await _repo.GetReviewByIdAsync(id);

            if (review == null) return NotFound();

            var email = User.FindFirstValue(ClaimTypes.Email);

            var user = await _userManager.FindByEmailAsync(email);

            if (user.Id != review.UserId) return Unauthorized();

            await _repo.DeleteReviewAsync(review);

            return NoContent();
        }
    }
}
