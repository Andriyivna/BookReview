using System.Collections.Generic;

namespace API.Entities
{
    public class VirtualLibrary
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public ICollection<VirtualLibraryBook> Books { get; set; }
    }
}
