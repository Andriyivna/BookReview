using System.ComponentModel.DataAnnotations;

namespace API.Dtos
{
    public class AuthorUpdateDto
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string SecondName { get; set; }
    }
}
