using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookReview.DataBase.DataBaseModels
{
    public class MyShelf
    {

        public int ID { get; set; }
        public string Status { get; set; }

        public int UserId { get; set; }
        public int BooksId { get; set; }

        public Books Books { get; set; }
        public User User { get; set; }

    }
}
