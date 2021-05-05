using API.Data;
using API.Dtos;
using API.Entities;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
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
        public IActionResult AddBook(BetterBook book)
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
            var books = _context.Books.ToList();
            var dtobook = _mapper.Map<IReadOnlyList<Book>>(books);
            return Ok(dtobook);
        }
        [HttpGet("id/{id}")]
        public ActionResult<BetterBook> GetABookWithID(int id)
        {
            var book = _context.Books.FirstOrDefault(p => p.Id == id);
            if (book == null)
            {
                return NotFound();
            }
            var dtobook = _mapper.Map<Book>(book);
            return Ok(dtobook);
        }
        [HttpGet("title/{title}")]
        public ActionResult<BetterBook> GetABookWithTitle(string title)
        {
            var book = _context.Books.FirstOrDefault(p => p.Title == title);
            if (book == null)
            {
                return NotFound();
            }
            var dtobook = _mapper.Map<Book>(book);
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
            var book = _context.Books.FirstOrDefault(p => p.Id == id);
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
            var book = _context.Books.FirstOrDefault(p => p.Title == title);
            if (book == null)
            {
                return NotFound();
            }
            var newbook = _mapper.Map<Book>(book);
            _context.Books.Remove(newbook);
            _context.SaveChanges();
            return NoContent();
        }

        [HttpGet("sort/rate/{order}")]
        public ActionResult<IReadOnlyList<BetterBook>> SortBooksWithRatingDescending(string order)
        {
            int maxID = _context.Reviews.Max(u=>u.Id);

            for (int i = 1; i <= maxID; i++)
            {
                var revies = _context.Reviews.Where(q => q.Id == i).ToList();
                double averangeRate = 0;
                foreach (var item in revies)
                {
                    averangeRate += item.GivenRate;
                }
                double rate = averangeRate / revies.Count;
                var book = _context.Books.Where(q => q.Id == i).FirstOrDefault();
                book.AverangeRates = rate;
                _context.Books.Update(book);
            }
            if (order == "Descending")
            {
                var sortedBooks = _context.Books.OrderByDescending(q => q.AverangeRates).ToList();
                var dtobook = _mapper.Map<IReadOnlyList<Book>>(sortedBooks);
                return Ok(dtobook);

            }
            if (order == "Ascending")
            {
                var sortedBooks = _context.Books.OrderBy(q=>q.Id).ToList();
                var dtobook = _mapper.Map<IReadOnlyList<Book>>(sortedBooks);
                return Ok(dtobook);

            }
            return BadRequest();
           
        }
        [HttpGet("sort/release")]
        public ActionResult<IReadOnlyList<BetterBook>> SortBooksWithRelease()
        {
            var sortedBooks = _context.Books.OrderByDescending(q => q.ReleaseYear).ToList();
            var dtobook = _mapper.Map<IReadOnlyList<Book>>(sortedBooks);
            return Ok(dtobook);
        }
        [HttpGet("filtreGenre/{genre}")]
        public ActionResult<IReadOnlyList<BetterBook>> FiltreBooksWithGenre(string genre)
        {
            var filtredBooks = _context.Books.Where(q => q.Genre.Name==genre).ToList();
            var dtobook = _mapper.Map<IReadOnlyList<Book>>(filtredBooks);
            return Ok(dtobook);
        }
        [HttpGet("filtreAuthor/{author}")]
        public ActionResult<IReadOnlyList<BetterBook>> FiltreBooksWithAuthor(string author)
        {
            var filtredBooks = _context.Books.Where(q => q.Author.FirstName == author).ToList();
            var dtobook = _mapper.Map<IReadOnlyList<Book>>(filtredBooks);
            return Ok(dtobook);
        }
    }
}
