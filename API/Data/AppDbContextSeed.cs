using API.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Data
{
    public class AppDbContextSeed
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

        public static async Task SeedDataAsync(AppDbContext context, ILoggerFactory loggerFactory)
        {
            try
            {
                if (!context.Authors.Any())
                {
                    var authors = new List<Author>
                    {
                        new Author
                        {
                            FirstName = "John",
                            SecondName = "Fowels"
                        },
                        new Author
                        {
                            FirstName = "Kurt",
                            SecondName = "Vonnegut"
                        },
                        new Author
                        {
                            FirstName = "Terry",
                            SecondName = "Pratchett"
                        }
                    };

                    context.Authors.AddRange(authors);

                    await context.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                var logger = loggerFactory.CreateLogger<AppDbContextSeed>();
                logger.LogError(ex.Message);
            }
        }
    }
}
