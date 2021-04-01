using BookReview.Core.Repositories;
using BookReview.DataBase.DataBaseModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BookReview.Infrastructure.Extensions
{
    public static class RepositoryExtensions
    {
        public static async Task<Books> GetOrFailAsync(this IBooksRepository repository, int id)
        {
            var book = await repository.GetAsync(id);
            if(book==null)
            {
                throw new ApplicationException($"Book with id '{id}' does not exist.");
            }
            return book;
        }
        public static async Task<Books> GetOrFailAsync(this IBooksRepository repository, string title)
        {
            var book = await repository.GetAsync(title);
            if (book == null)
            {
                throw new ApplicationException($"Book with Title '{title}' does not exist.");
            }
            return book;
        }
    }
}
