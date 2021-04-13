using System.ComponentModel.DataAnnotations;

namespace API.Dtos
{
    public class AuthorAddDto
    {
        [Required]
        public string FirstName { get; set; }

        [Required]
        public string SecondName { get; set; }
    }
}
