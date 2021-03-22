using System.Collections.Generic;

namespace API.Entities
{
    public class Author
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string SecondName { get; set; }
        public ICollection<Book> Books { get; set; }
    }
}
