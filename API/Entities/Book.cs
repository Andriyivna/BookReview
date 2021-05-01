using System.Collections.Generic;

namespace API.Entities
{
    public class Book
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string CoverImg { get; set; }
        public string Publisher { get; set; }
        public int ReleaseYear { get; set; }
        public string Description { get; set; }
        public int AuthorId { get; set; }
        public Author Author { get; set; }
        public int GenreId { get; set; }
        public Genre Genre { get; set; }
        public ICollection<User> UsersWhoFavouritedBook { get; set; }
    }
}
