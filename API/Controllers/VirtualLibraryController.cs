using API.Dtos;
using API.Entities;
using API.Entities.Enums;
using API.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Security.Claims;
using System.Threading.Tasks;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class VirtualLibraryController : ControllerBase
    {
        private readonly IVirtualLibrariesRepository _repo;
        private readonly IMapper _mapper;
        private readonly UserManager<User> _userManager;

        public VirtualLibraryController(IVirtualLibrariesRepository repo, IMapper mapper, UserManager<User> userManager)
        {
            _repo = repo;
            _mapper = mapper;
            _userManager = userManager;
        }

        [HttpGet]
        public async Task<ActionResult<VirtualLibraryDto>> GetVirtualLibrary()
        {
            var email = User.FindFirstValue(ClaimTypes.Email);

            var user = await _userManager.FindByEmailAsync(email);

            var virtualLib = await _repo.GetVirtualLibraryAsync(user.Id);

            var data = _mapper.Map<VirtualLibraryDto>(virtualLib);

            return Ok(data);
        }

        [HttpPost]
        public async Task<ActionResult<VirtualBookReturnDto>> AddBookToVirtualLibrary(VirtualBookAddDto bookToAdd)
        {
            var email = User.FindFirstValue(ClaimTypes.Email);

            var user = await _userManager.FindByEmailAsync(email);

            var book = _mapper.Map<VirtualLibraryBook>(bookToAdd);

            book.VirtualLibraryId = user.VirtualLibraryId;

            var addedBook = await _repo.AddBookToVirtualLibraryAsync(book);

            var addedBookWithIncludes = await _repo.GetVirtualLibraryBookByIdAsync(addedBook.Id, addedBook.VirtualLibraryId);

            var bookToReturn = _mapper.Map<VirtualBookReturnDto>(addedBookWithIncludes);

            return CreatedAtAction(nameof(GetVirtualLibrary), new { id = bookToReturn.Id }, bookToReturn);
        }

        [HttpPut]
        public async Task<ActionResult> EditBookInVirtualLibrary(VirtualBookUpdateDto bookToEdit)
        {
            var email = User.FindFirstValue(ClaimTypes.Email);

            var user = await _userManager.FindByEmailAsync(email);

            var virtualBookFromDb = await _repo.GetVirtualLibraryBookByIdAsync(bookToEdit.VirtualBookId, user.VirtualLibraryId);

            if (virtualBookFromDb == null) return NotFound();

            try
            {
                virtualBookFromDb.Status = (VirtualLibraryBookStatus)Enum.Parse(typeof(VirtualLibraryBookStatus), bookToEdit.Status);
            }
            catch (Exception)
            {
                return BadRequest("Entered status was not in correct format");
            }

            await _repo.EditBookInVirtualLibraryAsync(virtualBookFromDb);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteVirtualBook(int id)
        {
            var email = User.FindFirstValue(ClaimTypes.Email);

            var user = await _userManager.FindByEmailAsync(email);

            var book = await _repo.GetVirtualLibraryBookByIdAsync(id, user.VirtualLibraryId);

            if (book == null) return NotFound();

            await _repo.RemoveBookFromVirtualLibraryAsync(book);

            return NoContent();
        }
    }
}
