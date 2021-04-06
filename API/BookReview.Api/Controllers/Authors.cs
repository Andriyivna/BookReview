using BookReview.DataBase.DataBaseModels;
using BookReview.Infrastructure.DTO;
using BookReview.Infrastructure.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookReview.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Authors : ControllerBase
    {
        private IBooksServices _booksService;
        public Authors(IBooksServices booksServices)
        {
            _booksService = booksServices;
        }

        // POST: api/Authors
        [HttpPost]
        public async Task<IActionResult> CreateBook(Books book)
        {
            try
            {
                await _booksService.CreateAsync(book);
                return Ok();
            }
            catch (ApplicationException e)
            {
                return BadRequest(new { message = e.Message });
            }
        }
        // GET: api/Authors/title/{title}
        [HttpGet("id/{id}")]
        public async Task<IActionResult> GetBookWithID(int id)
        {
            try
            {
                BooksDTO book = await _booksService.GetBooksAsync(id);
                return Ok(new
                {
                    ID = book.ID,
                    Title = book.Title,
                    Author = book.Author,
                    Printer = book.Printer,
                    Description = book.Description
                });
            }
            catch (ApplicationException e)
            {
                return BadRequest(new { message = e.Message });
            }
        }
        // GET: api/Authors/title/{title}
        [HttpGet("title/{title}")]
        public async Task<IActionResult> GetBookWithTitle(string title)
        {
            try
            {
                BooksDTO book = await _booksService.GetBooksAsync(title);
                return Ok(new
                {
                    ID = book.ID,
                    Title = book.Title,
                    Author = book.Author,
                    Printer = book.Printer,
                    Description = book.Description
                });
            }
            catch (ApplicationException e)
            {
                return BadRequest(new { message = e.Message });
            }
        }
        // PUT: api/Authors/title/{title}
        [HttpPut("id/{id}")]
        public async Task<IActionResult> UpdateBook(int id, [FromBody] Books book)
        {
            try
            {
                await _booksService.UpdateAsync(id, book.Author);
                return Ok();
            }
            catch (ApplicationException e)
            {
                return BadRequest(new { message = e.Message });
            }
        }
        // PUT: api/Authors/title/{title}
        [HttpPut("title/{title}")]
        public async Task<IActionResult> GetBook(string title, [FromBody] Books book)
        {
            try
            {
                await _booksService.UpdateAsync(title, book.Author);
                return Ok();
            }
            catch (ApplicationException e)
            {
                return BadRequest(new { message = e.Message });
            }
        }
        // DELETE: api/Authors/id/{id}
        [HttpDelete("id/{id}")]
        public async Task<IActionResult> DeleteBook(int id)
        {
            try
            {
                await _booksService.DeleteAsync(id);
                return Ok();
            }
            catch (ApplicationException e)
            {
                return BadRequest(new { message = e.Message });
            }
        }
        // DELETE: api/Authors/title/{title}
        [HttpDelete("title/{title}")]
        public async Task<IActionResult> DeleteBook(string title)
        {
            try
            {
                await _booksService.DeleteAsync(title);
                return Ok();
            }
            catch (ApplicationException e)
            {
                return BadRequest(new { message = e.Message });
            }
        }
    }
}
