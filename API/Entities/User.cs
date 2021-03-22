namespace API.Entities
{
    public class User
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string DisplayName { get; set; }
        public string PasswordHash { get; set; }
        public int? FavouriteQuoteId { get; set; }
        public Quote FavouriteQuote { get; set; }
        public int? FavouriteBookId { get; set; }
        public Book FavouriteBook { get; set; }
        public int VirtualLibraryId { get; set; }
        public VirtualLibrary VirtualLibrary { get; set; }
    }
}