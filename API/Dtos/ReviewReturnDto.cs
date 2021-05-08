namespace API.Dtos
{
    public class ReviewReturnDto
    {
        public int Id { get; set; }
        public double GivenRate { get; set; }
        public string Content { get; set; }
        public string AddedAt { get; set; }
        public int BookId { get; set; }
    }
}
