namespace API.Dtos
{
    public class QuoteOfTheDayDto
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public string BookTitle { get; set; }
        public int BookId { get; set; }
        public string AuthorName { get; set; }
        public int AuthorId { get; set; }
        public string QuoteOfTheDayDuration { get; set; }
    }
}
