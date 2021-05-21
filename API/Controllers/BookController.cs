using API.Data;
using API.Dtos;
using API.Entities;
using API.Entities.Enums;
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
    public class BookController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public BookController(AppDbContext context,
            IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpPost("add")]
        public IActionResult AddBook(AddBook book)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            var newbook = _mapper.Map<Book>(book);
            _context.Books.Add(newbook);
            _context.SaveChanges();
            return Ok();
        }
        [HttpGet]
        public ActionResult<IReadOnlyList<BetterBook>> GetAllBooks()
        {
            var books = _context.Books.Include(a=>a.Author).Include(g=>g.Genre).ToList();
            var dtobook = _mapper.Map<IReadOnlyList<BetterBook>>(books);
            
            return Ok(dtobook);
        }
        [HttpGet("high/rate/{count}")]
        public ActionResult<IReadOnlyList<BetterBook>> GetHighRatesBooks(int count)
        {
            var books = _context.Books.OrderByDescending(q=>q.AverageRates).Take(count).ToList();
            var dtobook = _mapper.Map<IReadOnlyList<BetterBook>>(books);

            return Ok(dtobook);
        }
        [HttpGet("id/{id}")]
        public ActionResult<BetterBook> GetABookWithID(int id)
        {
            var book = _context.Books.Include(a => a.Author).Include(g => g.Genre).FirstOrDefault(p => p.Id == id);
            if (book == null)
            {
                return NotFound();
            }
            var dtobook = _mapper.Map<BetterBook>(book);
            return Ok(dtobook);
        }
        

        [HttpPut]
        public IActionResult UpdateBook(BetterBook book)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            var newbook = _mapper.Map<Book>(book);
            _context.Books.Update(newbook);
            _context.SaveChanges();
            return Ok();
        }

        [HttpDelete("id/{id}")]
        public IActionResult RemoveBookWithID(int id)
        {
            var book = _context.Books.Include(a => a.Author).Include(g => g.Genre).FirstOrDefault(p => p.Id == id);
            if (book == null)
            {
                return NotFound();
            }
            var newbook = _mapper.Map<Book>(book);
            _context.Books.Remove(newbook);
            _context.SaveChanges();
            return NoContent();
        }
        [HttpDelete("title/{title}")]
        public IActionResult RemoveBookWithTitle(string title)
        {
            var book = _context.Books.Include(a => a.Author).Include(g => g.Genre).FirstOrDefault(p => p.Title == title);
            if (book == null)
            {
                return NotFound();
            }
            var newbook = _mapper.Map<Book>(book);
            _context.Books.Remove(newbook);
            _context.SaveChanges();
            return NoContent();
        }

        [HttpGet("title/{title}")]
        public ActionResult<BetterBook> GetABookWithTitle(string title)
        {
            var book = _context.Books.Include(a => a.Author).Include(g => g.Genre).FirstOrDefault(p => p.Title == title);
            if (book == null)
            {
                return NotFound();
            }
            var dtobook = _mapper.Map<BetterBook>(book);
            return Ok(dtobook);
        }

        [HttpGet("sort/rate/{order}")]
        public ActionResult<IReadOnlyList<BetterBook>> SortBooksWithRatingDescending(string order)
        {
            int maxID = _context.Books.Max(u=>u.Id);
            int minID = _context.Books.Min(u => u.Id);

            for (int i = minID; i <= maxID; i++)
            {
                var revies = _context.Reviews.Where(q => q.BookId == i).ToList();
                if (revies.Count>0)
                {
                    double averangeRate = 0;
                    foreach (var item in revies)
                    {
                        averangeRate += item.GivenRate;
                    }
                    double rate = averangeRate / revies.Count;
                    var book = _context.Books.Where(q => q.Id == i).FirstOrDefault();
                    if (book != null)
                    {
                        book.AverageRates = rate;
                        _context.Books.Update(book);
                        _context.SaveChanges();
                    }
                }
            }
            if (order == "Descending")
            {
                var sortedBooks = _context.Books.Include(a => a.Author).Include(g => g.Genre).OrderByDescending(q => q.AverageRates).ToList();
                var dtobook = _mapper.Map<IReadOnlyList<BetterBook>>(sortedBooks);
                return Ok(dtobook);

            }
            if (order == "Ascending")
            {
                var sortedBooks = _context.Books.Include(a => a.Author).Include(g => g.Genre).OrderBy(q=>q.AverageRates).ToList();
                var dtobook = _mapper.Map<IReadOnlyList<BetterBook>>(sortedBooks);
                return Ok(dtobook);
              
            }
            return BadRequest();
           
        }
        [HttpGet("sort/release")]
        public ActionResult<IReadOnlyList<BetterBook>> SortBooksWithRelease()
        {
            var sortedBooks = _context.Books.Include(a => a.Author).Include(g => g.Genre).OrderByDescending(q => q.ReleaseYear).ToList();
            var dtobook = _mapper.Map<IReadOnlyList<BetterBook>>(sortedBooks);
            return Ok(dtobook);
        }
        [HttpGet("filtreGenre/{genre}")]
        public ActionResult<IReadOnlyList<BetterBook>> FiltreBooksWithGenre(string genre)
        {
            var filtredBooks = _context.Books.Include(a => a.Author).Include(g => g.Genre).Where(q => q.Genre.Name==genre).ToList();
            var dtobook = _mapper.Map<IReadOnlyList<BetterBook>>(filtredBooks);
            return Ok(dtobook);
        }
        [HttpGet("filtreAuthor/{author}")]
        public ActionResult<IReadOnlyList<BetterBook>> FiltreBooksWithAuthor(string author)
        {
            List<Book> filtredBooks = _context.Books.Include(a => a.Author).Include(g => g.Genre).Where(q => q.Author.FirstName.ToLower().Contains(author.ToLower())).ToList();
            filtredBooks.AddRange(_context.Books.Include(a => a.Author).Include(g => g.Genre).Where(q => q.Author.SecondName.ToLower().Contains(author.ToLower())).ToList());
            var dtobook = _mapper.Map<IReadOnlyList<BetterBook>>(filtredBooks);
            return Ok(dtobook);
        }
        [HttpGet("filtreTitle/{title}")]
        public ActionResult<IReadOnlyList<BetterBook>> FiltreBooksWithTitle(string title)
        {
            List<Book> filtredBooks = _context.Books.Include(a => a.Author).Include(g => g.Genre).Where(q => q.Title.ToLower().Contains(title.ToLower())).ToList();
            if (filtredBooks == null)
            {
                return NotFound();
            }
            var dtobook = _mapper.Map<IReadOnlyList<BetterBook>>(filtredBooks);
            return Ok(dtobook);
        }

    }
}
