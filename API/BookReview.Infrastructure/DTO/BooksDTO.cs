using System;
using System.Collections.Generic;
using System.Text;

namespace BookReview.Infrastructure.DTO
{
    public class BooksDTO
    {
        public int ID { get; set; }
        public string Title { get;  set; }
        public string Author { get; set; }
        public string Printer { get; set; }
        public string Description { get;  set; }

    }
}
