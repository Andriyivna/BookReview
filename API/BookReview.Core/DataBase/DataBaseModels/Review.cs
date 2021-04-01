using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookReview.DataBase.DataBaseModels
{
    public class Review
    {
        public int ID { get; set; }
        public string Content { get; set; }
        public float Rate { get; set; }
        public int BooksId { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public Books Books { get; set; }
    }
}
