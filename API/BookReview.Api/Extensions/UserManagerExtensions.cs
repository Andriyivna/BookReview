using API.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using System.Threading.Tasks;

namespace API.Extensions
{
    public static class UserManagerExtensions
    {
        public static async Task<User> FindByEmailWithFavouritesAndVirtualLibrary(this UserManager<User> input, ClaimsPrincipal user)
        {
            var email = user.FindFirstValue(ClaimTypes.Email);

            return await input.Users
                .Include(x => x.FavouriteBook)
                .Include(x => x.FavouriteQuote)
                .Include(x => x.VirtualLibrary)
                .FirstOrDefaultAsync(x => x.Email == email);
        }
    }
}
