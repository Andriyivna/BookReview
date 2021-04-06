using Microsoft.AspNetCore.Identity;

namespace API.Entities
{
    public class User : IdentityUser
    {
        public string DisplayName { get; set; }
        public int? FavouriteQuoteId { get; set; }
        public Quote FavouriteQuote { get; set; }
        public int? FavouriteBookId { get; set; }
        public Book FavouriteBook { get; set; }
        public int VirtualLibraryId { get; set; }
        public VirtualLibrary VirtualLibrary { get; set; }
    }
}