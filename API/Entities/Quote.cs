using System;
using System.Collections.Generic;

namespace API.Entities
{
    public class Quote
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public Book Book { get; set; }
        public int BookId { get; set; }
        public Author Author { get; set; }
        public int AuthorId { get; set; }
        public bool IsQuoteOfTheDay { get; set; }
        public DateTime QuoteOfTheDayDuration { get; set; }
        public ICollection<User> UsersWhoFavouritedQuote { get; set; }
    }
}
