using API.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace API.Data
{
    public class AppDbContext : IdentityDbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base (options)
        {

        }

        public DbSet<Author> Authors { get; set; }
        public DbSet<Avatar> Avatars { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<Quote> Quotes { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<VirtualLibrary> VirtualLibraries { get; set; }
        public DbSet<VirtualLibraryBook> VirtualLibraryBooks { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>()
                .HasOne(x => x.FavouriteQuote)
                .WithMany(x => x.UsersWhoFavouritedQuote)
                .HasForeignKey(x => x.FavouriteQuoteId);

            modelBuilder.Entity<User>()
                .HasOne(x => x.FavouriteBook)
                .WithMany(x => x.UsersWhoFavouritedBook)
                .HasForeignKey(x => x.FavouriteBookId);

            modelBuilder.Entity<User>()
                .HasOne(x => x.VirtualLibrary)
                .WithOne(x => x.User)
                .HasForeignKey<VirtualLibrary>(x => x.UserId);
        }
    }
}
