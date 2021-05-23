using API.Data;
using API.Dtos;
using API.Entities;
using API.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
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
        private readonly AppDbContext _context;

        public ReviewsController(IReviewsRepository repo, UserManager<User> userManager, IMapper mapper, AppDbContext context)
        {
            _repo = repo;
            _userManager = userManager;
            _mapper = mapper;
            _context = context;
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
            foreach (var item in data)
            {
                var email = User.FindFirstValue(ClaimTypes.Email);
                var user = await _userManager.FindByEmailAsync(email);
                item.DisplayName = user.DisplayName;
                var avatar = _context.Avatars.Where(q => q.Id == user.AvatarId).FirstOrDefault();
                item.Avatar = avatar.Url;
                
            }
            return Ok(data);
        }
        [HttpGet("book/review/{user}")]
        public async Task<ActionResult<IReadOnlyList<ReviewReturnDto>>> GetReviewsForUser(string user)
        {
            var reviewsFromDb =_context.Reviews.Include(q=>q.Book).Where(q => q.User.DisplayName == user);

            var data = _mapper.Map<IReadOnlyList<ReviewReturnDto>>(reviewsFromDb);
            await Task.CompletedTask;
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
            var revies = _context.Reviews.Where(q => q.BookId == addedReview.BookId).ToList();
            if (revies.Count > 0)
            {
                double averangeRate = 0;
                foreach (var item in revies)
                {
                    averangeRate += item.GivenRate;
                }
                double rate = averangeRate / revies.Count;
                var book = _context.Books.Where(q => q.Id == addedReview.BookId).FirstOrDefault();
                if (book != null)
                {
                    book.AverageRates = rate;
                    _context.Books.Update(book);
                    _context.SaveChanges();
                }
            }
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
