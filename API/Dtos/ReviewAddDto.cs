using System.ComponentModel.DataAnnotations;

namespace API.Dtos
{
    public class ReviewAddDto
    {
        [Required]
        [Range(0, 10)]
        public double GivenRate { get; set; }

        [Required]
        public string Content { get; set; }

        [Required]
        public int BookId { get; set; }
    }
}
