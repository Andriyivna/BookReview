using API.Entities;
using API.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Data
{
    public class ReviewsRepository : IReviewsRepository
    {
        private readonly AppDbContext _context;

        public ReviewsRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Review> AddReviewAsync(Review review)
        {
            _context.Reviews.Add(review);
            await _context.SaveChangesAsync();

            return review;
        }

        public async Task DeleteReviewAsync(Review review)
        {
            _context.Reviews.Remove(review);
            await _context.SaveChangesAsync();
        }

        public async Task<Review> GetReviewByIdAsync(int id)
        {
            return await _context.Reviews.FindAsync(id);
        }

        public async Task<IReadOnlyList<Review>> GetReviewsAsync()
        {
            return await _context.Reviews
                .OrderByDescending(x => x.AddedAt)
                .ToListAsync();
        }

        public async Task<IReadOnlyList<Review>> GetReviewsByBookIdAsync(int bookId)
        {
            return await _context.Reviews
                .Where(x => x.BookId == bookId)
                .OrderByDescending(x => x.AddedAt)
                .ToListAsync();
        }

        public async Task<Review> UpdateReviewAsync(Review review)
        {
            _context.Entry(review).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return review;
        }
    }
}
