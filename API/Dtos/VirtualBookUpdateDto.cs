using System.ComponentModel.DataAnnotations;

namespace API.Dtos
{
    public class VirtualBookUpdateDto
    {
        [Required]
        public int VirtualBookId { get; set; }

        [Required]
        public string Status { get; set; }
    }
}
