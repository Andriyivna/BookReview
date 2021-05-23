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
                List<Author> authors = context.Authors.ToList();
                if (!context.Authors.Any(q=>q.SecondName== "Rowling"))
                {
                    authors = new List<Author>
                    {
                        new Author
                        {
                            FirstName = "Daniel",
                            SecondName = "Keyes"
                        },
                        new Author
                        {
                            FirstName = "J.K.",
                            SecondName = "Rowling"
                        },
                        new Author
                        {
                            FirstName = "Andrzej",
                            SecondName = "Spakowski"
                        },
                        new Author
                        {
                            FirstName = "J.R.R.",
                            SecondName = "Tolkien"
                        }
                    };

                    context.Authors.AddRange(authors);

                    await context.SaveChangesAsync();
                }
                List<Genre> genres = context.Genres.ToList();
                if (!context.Genres.Any(q=>q.Name== "Fantasy"))
                {
                    genres = new List<Genre>
                    {
                        new Genre
                        {
                            Name = "Literatura obyczajowa"
                        },
                        new Genre
                        {
                            Name = "Fantasy"
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

                if (!context.Books.Any(q=>q.Title== "Kwiaty dla Alegerona"))
                {

                    var books = new List<Book>
                    {
                        new Book
                        {
                            Title = "Kwiaty dla Alegerona",
                            AuthorId = authors[0].Id ,
                            CoverImg = "/assets/img/KwiatydlaAlgernona.jpg",
                            GenreId = genres[0].Id,
                            Publisher="Nowa Era",
                            Description="Trzydziestodwuletni Charlie Gordon jest upośledzony umysłowo, ma iloraz inteligencji na poziomie 68 punktów. Uczy się czytać i pisać na zajęciach w Beekman College. Dwaj naukowcy z tej uczelni, doktor Nemur i doktor Strauss, prowadzą badania nad wzrostem inteligencji. Udało im się już za pomocą zabiegu chirurgicznego zwiększyć zdolności umysłowe myszy o imieniu Algernon i teraz chcą przeprowadzić taki sam eksperyment z człowiekiem.",
                            ReleaseYear=2005
                        },
                        new Book
                        {
                            Title = "Harry Potter",
                            AuthorId = authors[1].Id ,
                            CoverImg = "/assets/img/harryPotter.jpg",
                            GenreId = genres[1].Id,
                            Publisher="Nowa Era",
                            Description= "Książka „Harry Potter i Kamień Filozoficzny” rozpoczyna cykl o młodym czarodzieju i jego licznych przygodach. Tytułowy Harry Potter wychowywany jest przez nieprzychylnych mu ciotkę i wuja. Jego rodzice zginęli w tajemniczych okolicznościach, a jedyne, co mu po nich pozostało to blizna na czole w kształcie błyskawicy. W dniu swoich 11. urodzin bohater dowiaduje się, że istnieje świat, o którym nie miał pojęcia",
                            ReleaseYear=2006
                        },
                         new Book
                        {
                            Title = "Miecz Przeznaczenia",
                            AuthorId = authors[2].Id ,
                            CoverImg = "/assets/img/wiedzmin.jpg",
                            GenreId = genres[1].Id,
                            Publisher="Mag",
                            Description= "Wiedźmiński kodeks stawia tę sprawę w sposób jednoznaczny: wiedźminowi smoka zabijać się nie godzi. To gatunek zagrożony wymarciem. Aczkolwiek w powszechnej opinii to gad najbardziej wredny. Na oszluzgi, widłogony i latawce kodeks polować przyzwala. Ale na smoki – nie. Wiedźmin Geralt przyłącza się jednak do zorganizowanej przez króla Niedamira wyprawy na smoka, który skrył się w jaskiniach Gór Pustulskich. Na swej drodze spotyka trubadura Jaskra oraz – jakżeby inaczej – czarodziejkę Yennefer. Wśród zaproszonych przez króla co sławniejszych smokobójców jest Eyck z Denesle, rycerz bez skazy i zmazy, Rębacze z Cinfrid i szóstka krasnoludów pod komendą Yarpena Zigrina. Motywacje są różne, ale cel jeden. Smok nie ma szans.",
                            ReleaseYear=2003
                        },
                          new Book
                        {
                            Title = "Drużyna pierścienia",
                            AuthorId = authors[3].Id ,
                            CoverImg = "/assets/img/bractwoPierscienia.jpg",
                            GenreId = genres[1].Id,
                            Publisher="Mag",
                            Description= "W zamierzchłych czasach kowale elfów wykuli Pierścienie Mocy. Lecz Mroczny Władca, stworzył w tajemnicy Jedyny Pierścień, napełniając go swą potęgą, aby rządził pozostałymi. Ale Pierścień zniknął i przepadł gdzieś w Śródziemiu.Minęło wiele wieków, zanim się odnalazł i trafi ł w ręce hobbita, którego przeznaczeniem stała się wędrówka do Krainy Cienia, by zniszczyć Jedyny Pierścień...",
                            ReleaseYear =1954
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

                if (!context.Quotes.Any())
                {
                    var mieczPrzezaczenia = await context.Books.Include(x => x.Author)
                        .Where(x => x.Title.ToLower() == "miecz przeznaczenia")
                        .FirstOrDefaultAsync();

                    var harryPotter = await context.Books.Include(x => x.Author)
                        .Where(x => x.Title.ToLower() == "harry potter")
                        .FirstOrDefaultAsync();

                    var kwiatyAlgernona = await context.Books.Include(x => x.Author)
                        .Where(x => x.Title.ToLower() == "kwiaty dla alegerona")
                        .FirstOrDefaultAsync();

                    var quotes = new List<Quote>
                    {
                        new Quote
                        {
                            Author = mieczPrzezaczenia.Author,
                            Book = mieczPrzezaczenia,
                            Content = "Nigdy nie ma się drugiej okazji, żeby zrobić pierwsze wrażenie."
                        },
                        new Quote
                        {
                            Author = mieczPrzezaczenia.Author,
                            Book = mieczPrzezaczenia,
                            Content = "Miecz przeznaczenia ma dwa ostrza. Jednym jesteś ty."
                        },
                        new Quote
                        {
                            Author = harryPotter.Author,
                            Book = harryPotter,
                            Content = "Patrząc na ciemność lub śmierć boimy się nieznanego - niczego więcej."
                        },
                        new Quote
                        {
                            Author = harryPotter.Author,
                            Book = harryPotter,
                            Content = "Niczego nie daje pogrążanie się w marzeniach i zapominanie o życiu."
                        },
                        new Quote
                        {
                            Author = kwiatyAlgernona.Author,
                            Book = kwiatyAlgernona,
                            Content = "Inteligencja i wykształcenie nie wzbogacone ludzkimi uczuciami nie mają żadnego znaczenia."
                        },
                        new Quote
                        {
                            Author = kwiatyAlgernona.Author,
                            Book = kwiatyAlgernona,
                            Content = "Boję się. Nie życia, nie śmierci, nie pustki, w której nie ma nic, lecz zniknięcia, jakbym nigdy nie istniał."
                        }
                    };

                    await context.Quotes.AddRangeAsync(quotes);
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
