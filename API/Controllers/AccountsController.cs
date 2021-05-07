using API.Dtos;
using API.Entities;
using API.Extensions;
using API.Interfaces;
using AutoMapper;
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
        private readonly IVirtualLibrariesRepository _virtualLibRepo;
        private readonly IMapper _mapper;

        public AccountsController(UserManager<User> userManager, SignInManager<User> signInManager, ITokenService tokenService, IVirtualLibrariesRepository virtualLibRepo, IMapper mapper)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _tokenService = tokenService;
            _virtualLibRepo = virtualLibRepo;
            _mapper = mapper;
        }

        [HttpPost("login")]
        public async Task<ActionResult<UserDto>> Login(LoginDto loginDto)
        {
            var user = await _userManager.FindByEmailWithFavouritesAndVirtualLibrary(loginDto.Email);

            if (user == null) return Unauthorized();

            var result = await _signInManager.CheckPasswordSignInAsync(user, loginDto.Password, true);

            if (!result.Succeeded) return Unauthorized();

            var userDto = _mapper.Map<UserDto>(user);

            userDto.Token = _tokenService.CreateToken(user);

            return userDto;
        }

        [HttpPost("register")]
        public async Task<ActionResult<UserDto>> Register(RegisterDto registerDto)
        {
            var user = new User
            {
                DisplayName = registerDto.DisplayName,
                Email = registerDto.Email,
                UserName = registerDto.Email,
                AvatarId = registerDto.AvatarId
            };

            var result = await _userManager.CreateAsync(user, registerDto.Password);

            if (!result.Succeeded) return BadRequest();

            var virtualLib = await _virtualLibRepo.CreateVirtualLibrary(user.Id);

            user.VirtualLibraryId = virtualLib.Id;

            await _userManager.UpdateAsync(user);

            var userToMap = await _userManager.FindByEmailWithFavouritesAndVirtualLibrary(user.Email);

            var userDto = _mapper.Map<UserDto>(userToMap);

            userDto.Token = _tokenService.CreateToken(userToMap);

            return userDto;
        }

        [HttpGet]
        [Authorize]
        public async Task<ActionResult<UserDto>> GetCurrentUser()
        {
            var email = User.FindFirstValue(ClaimTypes.Email);

            var user = await _userManager.FindByEmailWithFavouritesAndVirtualLibrary(email);

            return _mapper.Map<UserDto>(user);
        }

        [HttpGet("emailexists")]
        public async Task<ActionResult<bool>> CheckIfEmailExistsAsync([FromQuery] string email)
        {
            return await _userManager.FindByEmailAsync(email) != null;
        }
    }
}
