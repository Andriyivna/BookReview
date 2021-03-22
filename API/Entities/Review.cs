namespace API.Entities
{
    public class Review
    {
        public int Id { get; set; }
        public double GivenRate { get; set; }
        public string Content { get; set; }
        public int BookId { get; set; }
        public Book Book { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
    }
}
