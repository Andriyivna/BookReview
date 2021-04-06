using AutoMapper;
using BookReview.Core.Repositories;
using BookReview.DataBase.DataBaseModels;
using BookReview.Infrastructure.DTO;
using BookReview.Infrastructure.Extensions;
using System;
using System.Threading.Tasks;

namespace BookReview.Infrastructure.Services
{
    public class BooksServices : IBooksServices
    {
        private readonly IBooksRepository _booksRepository;
        private readonly IMapper _mapper;

        public BooksServices(IBooksRepository booksRepositoriy, IMapper mapper)
        {
            _booksRepository = booksRepositoriy;
            _mapper = mapper;
        }

        public async Task CreateAsync(Books book)
        {
            var newbook = await _booksRepository.GetAsync(book.ID);
            if (newbook != null)
            {
                throw new ApplicationException($"Book with ID '{book.ID}' already exist.");
            }
            newbook = await _booksRepository.GetAsync(book.Title);
            if (newbook != null)
            {
                throw new ApplicationException($"Book with Title '{book.Title}' already exist.");
            }
            newbook = new Books(book.Title, book.Author, book.Printer, book.Description);
            await _booksRepository.CreateAsync(newbook);
        }

        public async Task<BooksDTO> GetBooksAsync(int id)
        {
            var book = await _booksRepository.GetOrFailAsync(id);
            return _mapper.Map<BooksDTO>(book);
        }

        public async Task<BooksDTO> GetBooksAsync(string title)
        {
            var book = await _booksRepository.GetOrFailAsync(title);
            return _mapper.Map<BooksDTO>(book);
        }

        public async Task UpdateAsync(int id, string author)
        {
            await _booksRepository.GetOrFailAsync(id);
            await _booksRepository.UpdateAsync(id, author);
        }

        public async Task UpdateAsync(string title, string author)
        {
            await _booksRepository.GetOrFailAsync(title);
            await _booksRepository.UpdateAsync(title, author);
        }
        public async Task DeleteAsync(int id)
        {
            await _booksRepository.GetOrFailAsync(id);
            await _booksRepository.DeleteAsync(id);
        }

        public async Task DeleteAsync(string title)
        {
            await _booksRepository.GetOrFailAsync(title);
            await _booksRepository.DeleteAsync(title);
        }
    }
}
