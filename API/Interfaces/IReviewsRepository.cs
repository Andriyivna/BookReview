using API.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace API.Interfaces
{
    public interface IReviewsRepository
    {
        Task<Review> GetReviewByIdAsync(int id);
        Task<IReadOnlyList<Review>> GetReviewsByBookIdAsync(int bookId);
        Task<IReadOnlyList<Review>> GetReviewsAsync();
        Task<Review> AddReviewAsync(Review review);
        Task<Review> UpdateReviewAsync(Review review);
        Task DeleteReviewAsync(Review review);
    }
}
