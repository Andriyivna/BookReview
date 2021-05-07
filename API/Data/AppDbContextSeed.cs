using API.Entities;
using API.Entities.Enums;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
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

        public static async Task SeedDataAsync(AppDbContext context, UserManager<User> userManager, ILoggerFactory loggerFactory)
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

                if (!context.Genres.Any())
                {
                    var genres = new List<Genre>
                    {
                        new Genre
                        {
                            Name = "Literatura obyczajowa"
                        },
                        new Genre
                        {
                            Name = "Romans"
                        },
                        new Genre
                        {
                            Name = "Kryminał"
                        },
                        new Genre
                        {
                            Name = "Science finction"
                        },
                    };

                    context.Genres.AddRange(genres);

                    await context.SaveChangesAsync();
                }

                if (!context.Books.Any())
                {
                    var author = await context.Authors.FirstOrDefaultAsync();

                    var genre = await context.Genres.FirstOrDefaultAsync();

                    var books = new List<Book>
                    {
                        new Book
                        {
                            Title = "Książka testowa",
                            AuthorId = author.Id,
                            CoverImg = "https://placekitten.com/250/250",
                            GenreId = genre.Id
                        },
                        new Book
                        {
                            Title = "Książka testowa 2",
                            AuthorId = author.Id,
                            CoverImg = "https://placekitten.com/300/300",
                            GenreId = genre.Id
                        },
                    };

                    context.Books.AddRange(books);

                    await context.SaveChangesAsync();
                }

                var usersWithNoLibrary = await userManager.Users.Where(x => x.VirtualLibraryId == 0).ToListAsync();

                var virtualLibrariesToAdd = new List<VirtualLibrary>();

                foreach (var user in usersWithNoLibrary)
                {
                    var library = new VirtualLibrary
                    {
                        UserId = user.Id
                    };

                    user.VirtualLibrary = library;

                    virtualLibrariesToAdd.Add(library);
                }

                if (usersWithNoLibrary.Count > 0)
                {
                    context.VirtualLibraries.AddRange(virtualLibrariesToAdd);

                    await context.SaveChangesAsync();

                    usersWithNoLibrary.ForEach(x => x.VirtualLibraryId = x.VirtualLibrary.Id);

                    await context.SaveChangesAsync();
                }

                var admin = await userManager.FindByEmailAsync("admin@admin.com");

                if (admin != null)
                {
                    var virtualLibrary = await context.VirtualLibraries
                        .Include(x => x.Books)
                        .Where(x => x.Id == admin.VirtualLibraryId)
                        .FirstOrDefaultAsync();

                    if (virtualLibrary.Books.Count == 0)
                    {
                        var books = await context.Books.Take(3).ToListAsync();

                        var virtualLibraryBooks = new List<VirtualLibraryBook>();

                        foreach (var book in books)
                        {
                            virtualLibraryBooks.Add(new VirtualLibraryBook
                            {
                                BookId = book.Id,
                                VirtualLibraryId = virtualLibrary.Id,
                                Status = VirtualLibraryBookStatus.InProgress
                            });
                        };

                        context.VirtualLibraryBooks.AddRange(virtualLibraryBooks);

                        await context.SaveChangesAsync();
                    }
                }

                if (!context.Avatars.Any())
                {
                    var avatars = new List<Avatar>
                    {
                        new Avatar{Url = "avatars/av-female.jpg"},
                        new Avatar{Url = "avatars/av-female-2.jpg"},
                        new Avatar{Url = "avatars/av-female-3.jpg"},
                        new Avatar{Url = "avatars/av-male.jpg"},
                        new Avatar{Url = "avatars/av-male-2.jpg"},
                        new Avatar{Url = "avatars/av-male-3.jpg"}
                    };

                    context.AddRange(avatars);

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
