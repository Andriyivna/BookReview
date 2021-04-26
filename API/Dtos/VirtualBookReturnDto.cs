namespace API.Dtos
{
    public class VirtualBookReturnDto
    {
        public int Id { get; set; }
        public int BookId { get; set; }
        public string Title { get; set; }
        public string CoverImg { get; set; }
        public string Author { get; set; }
        public string Genre { get; set; }
        public string Status { get; set; }
    }
}
