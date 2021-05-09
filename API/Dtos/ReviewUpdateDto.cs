using System.ComponentModel.DataAnnotations;

namespace API.Dtos
{
    public class ReviewUpdateDto
    {
        [Required]
        public int Id { get; set; }

        [Required]
        [Range(0, 10)]
        public double GivenRate { get; set; }

        [Required]
        public string Content { get; set; }
    }
}
