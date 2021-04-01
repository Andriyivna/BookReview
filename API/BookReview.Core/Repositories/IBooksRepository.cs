using BookReview.DataBase.DataBaseModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BookReview.Core.Repositories
{
    public interface IBooksRepository
    {
        Task CreateAsync(Books book);
        Task<Books> GetAsync(int id);
        Task<Books> GetAsync(string title);
        Task UpdateAsync(int id, string author);
        Task UpdateAsync(string title, string author);
        Task DeleteAsync(int id);
        Task DeleteAsync(string title);
    }
}