using API.Entities.Enums;

namespace API.Entities
{
    public class VirtualLibraryBook
    {
        public int Id { get; set; }
        public int VirtualLibraryId { get; set; }
        public VirtualLibrary VirtualLibrary { get; set; }
        public int BookId { get; set; }
        public Book Book { get; set; }
        public VirtualLibraryBookStatus Status { get; set; }
    }
}