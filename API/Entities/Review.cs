using System;

namespace API.Entities
{
    public class Review
    {
        public int Id { get; set; }
        public double GivenRate { get; set; }
        public string Content { get; set; }
        public DateTime AddedAt { get; set; }
        public int BookId { get; set; }
        public Book Book { get; set; }
        public string UserId { get; set; }
        public User User { get; set; }
    }
}
