using API.Entities;
using Microsoft.AspNetCore.Identity;
using System.Linq;
using System.Threading.Tasks;

namespace API.Data
{
    public class AppDbContextSeedIdentity
    {
        public static async Task SeedUsersAsync(UserManager<User> userManager)
        {
            if (!userManager.Users.Any())
            {
                var user = new User
                {
                    DisplayName = "Admin",
                    Email = "admin@admin.com",
                    UserName = "admin@admin.com"
                };

                await userManager.CreateAsync(user, "Pa$$w0rd");
            }
        }
    }
}
