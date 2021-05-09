namespace API.Dtos
{
    public class UserUpdateDto
    {
        public string Email { get; set; }
        public string DisplayName { get; set; }
        public int? AvatarId { get; set; }
    }
}
