using API.Dtos;
using API.Entities;
using API.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Threading.Tasks;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly ITokenService _tokenService;
<<<<<<< HEAD

        public AccountsController(UserManager<User> userManager, SignInManager<User> signInManager, ITokenService tokenService)
=======
        private readonly IVirtualLibrariesRepository _virtualLibRepo;

        public AccountsController(UserManager<User> userManager, SignInManager<User> signInManager, ITokenService tokenService, IVirtualLibrariesRepository virtualLibRepo)
>>>>>>> ccae311812251f519d42b999b10c866d9fb3c0df
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _tokenService = tokenService;
<<<<<<< HEAD
=======
            _virtualLibRepo = virtualLibRepo;
>>>>>>> ccae311812251f519d42b999b10c866d9fb3c0df
        }

        [HttpPost("login")]
        public async Task<ActionResult<UserDto>> Login(LoginDto loginDto)
        {
            var user = await _userManager.FindByEmailAsync(loginDto.Email);

            if (user == null) return Unauthorized();

            var result = await _signInManager.CheckPasswordSignInAsync(user, loginDto.Password, true);

            if (!result.Succeeded) return Unauthorized();

            return new UserDto
            {
                DisplayName = user.DisplayName,
                Email = user.Email,
                Token = _tokenService.CreateToken(user)
            };
        }

        [HttpPost("register")]
        public async Task<ActionResult<UserDto>> Register(RegisterDto registerDto)
        {
            var user = new User
            {
                DisplayName = registerDto.DisplayName,
                Email = registerDto.Email,
                UserName = registerDto.Email
            };

            var result = await _userManager.CreateAsync(user, registerDto.Password);

            if (!result.Succeeded) return BadRequest();

<<<<<<< HEAD
=======
            var virtualLib = await _virtualLibRepo.CreateVirtualLibrary(user.Id);

            user.VirtualLibraryId = virtualLib.Id;

            await _userManager.UpdateAsync(user);

>>>>>>> ccae311812251f519d42b999b10c866d9fb3c0df
            return new UserDto
            {
                DisplayName = user.DisplayName,
                Email = user.Email,
                Token = _tokenService.CreateToken(user)
            };
        }

        [HttpGet]
        [Authorize]
        public async Task<ActionResult<UserDto>> GetCurrentUser()
        {
            var email = User.FindFirstValue(ClaimTypes.Email);

            var user = await _userManager.FindByEmailAsync(email);

            return new UserDto
            {
                Email = user.Email,
                Token = _tokenService.CreateToken(user),
                DisplayName = user.DisplayName
            };
        }

        [HttpGet("emailexists")]
        public async Task<ActionResult<bool>> CheckIfEmailExistsAsync([FromQuery] string email)
        {
            return await _userManager.FindByEmailAsync(email) != null;
        }
    }
}
