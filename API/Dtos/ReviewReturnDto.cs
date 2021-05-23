namespace API.Dtos
{
    public class ReviewReturnDto
    {
        public int Id { get; set; }
        public string DisplayName { get; set; }
        public string Avatar { get; set; }
        public double GivenRate { get; set; }
        public string Content { get; set; }
        public string AddedAt { get; set; }
        public int BookId { get; set; }
        public string BookTitle { get; set; }
    }
}
