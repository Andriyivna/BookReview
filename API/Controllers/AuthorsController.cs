using API.Dtos;
using API.Entities;
using API.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorsController : ControllerBase
    {
        private readonly IAuthorsRepository _repo;
        private readonly IMapper _mapper;

        public AuthorsController(IAuthorsRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<AuthorReturnDto>>> GetAuthors()
        {
            var authorsFromDb = await _repo.GetAuthorsAsync();

            var data = _mapper.Map<IReadOnlyList<AuthorReturnDto>>(authorsFromDb);

            return Ok(data);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<AuthorReturnDto>> GetAuthorById(int id)
        {
            var authorFromDb = await _repo.GetAuthorByIdAsync(id);

            var data = _mapper.Map<AuthorReturnDto>(authorFromDb);

            return Ok(data);
        }

        [HttpPost]
        public async Task<ActionResult<AuthorReturnDto>> AddAuthor(AuthorAddDto authorToAdd)
        {
            var author = _mapper.Map<Author>(authorToAdd);

            var addedAutor = await _repo.AddAuthorAsync(author);

            var authorToReturn = _mapper.Map<AuthorReturnDto>(addedAutor);

            return CreatedAtAction(nameof(GetAuthorById), new { id = authorToReturn.Id }, authorToReturn);
        }

        [HttpPut]
        public async Task<ActionResult<AuthorReturnDto>> UpdateAuthor(AuthorUpdateDto authorToUpdate)
        {
            var author = await _repo.GetAuthorByIdAsync(authorToUpdate.Id);

            if (author == null) return NotFound();

            author.FirstName = authorToUpdate.FirstName;
            author.SecondName = authorToUpdate.SecondName;

            await _repo.UpdateAuthorAsync(author);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteAuthor(int id)
        {
            var author = await _repo.GetAuthorByIdAsync(id);

            if (author == null) return NotFound();

            await _repo.DeleteAuthorAsync(author);

            return NoContent();
        }
    }
}
