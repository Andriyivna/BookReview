using BookReview.DataBase.DataBaseModels;
using BookReview.Infrastructure.DTO;
using System.Threading.Tasks;

namespace BookReview.Infrastructure.Services
{
    public interface IBooksServices
    {
        Task<BooksDTO> GetBooksAsync(int id);
        Task<BooksDTO> GetBooksAsync(string title);
        Task DeleteAsync(int id);
        Task DeleteAsync(string title);
        Task CreateAsync(Books book);
        Task UpdateAsync(int id, string author);
        Task UpdateAsync(string title, string author);
    }
}
