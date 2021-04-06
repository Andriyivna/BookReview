using BookReview.Core.Repositories;
using BookReview.DataBase;
using BookReview.DataBase.DataBaseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookReview.Infrastructure.Repositories
{
   public class BooksRepository : IBooksRepository
    {
        private readonly MyBase _context;
        public BooksRepository(MyBase context)
        {
            _context = context;
        }
        public async Task CreateAsync(Books book)
        {
            _context.Books.Add(book);
            _context.SaveChanges();
            await Task.CompletedTask;
        }

        public async Task<Books> GetAsync(int id)
            => await Task.FromResult(_context.Books.SingleOrDefault(x =>
                    x.ID == id));


        public async Task<Books> GetAsync(string title)
        => await Task.FromResult(_context.Books.SingleOrDefault(x =>
                    x.Title.ToLower() == title.ToLower()));

        public async Task UpdateAsync(int id, string author)
        {
            var book  = GetBook(id);
            book.Author = author;
            _context.Books.Update(book);
            _context.SaveChanges();
            await Task.CompletedTask;
        }

        public async Task UpdateAsync(string title, string author)
        {
            var book = GetBook(title);
            book.Author = author;
            _context.Books.Update(book);
            _context.SaveChanges();
            await Task.CompletedTask;
        }
        public async Task DeleteAsync(int id)
        {
            var book = GetBook(id);
            _context.Books.Remove(book);
            _context.SaveChanges();
            await Task.CompletedTask;
        }

        public async Task DeleteAsync(string title)
        {
            var book = GetBook(title);
            _context.Books.Remove(book);
            _context.SaveChanges();
            await Task.CompletedTask;

        }

        private Books GetBook(string title)
        {
            return _context.Books.FirstOrDefault(x =>
                  x.Title.ToLower() == title.ToLower());
        }
        private Books GetBook(int id)
        {
            return _context.Books.FirstOrDefault(x =>
                     x.ID == id);
        }
    }
}
