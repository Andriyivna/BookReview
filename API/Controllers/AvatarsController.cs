using API.Data;
using API.Dtos;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AvatarsController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public AvatarsController(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<AvatarDto>>> GetAvatars()
        {
            var avatars = await _context.Avatars.ToListAsync();

            var data = _mapper.Map<IReadOnlyCollection<AvatarDto>>(avatars);

            return Ok(data);
        }
    }
}
