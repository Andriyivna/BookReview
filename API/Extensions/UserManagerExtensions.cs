using API.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace API.Extensions
{
    public static class UserManagerExtensions
    {
        public static async Task<User> FindByEmailWithFavouritesAndVirtualLibrary(this UserManager<User> input, string email)
        {
            return await input.Users
                .Include(x => x.FavouriteBook)
                .Include(x => x.FavouriteQuote)
                .Include(x => x.VirtualLibrary)
                .Include(x => x.Avatar)
                .FirstOrDefaultAsync(x => x.Email == email);
        }
    }
}
