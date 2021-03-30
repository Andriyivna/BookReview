using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookReview.DataBase.DataBaseModels
{
    public class Books
    {
        public int ID { get; set; }
        public string Title { get; set; }
        public string Author{ get; set; }
        public string Printer { get; set; }
        public string Description { get; set; }
        public List<MyShelf> MyShelf { get; set; }
        public List<Review> Review { get; set; }


    }
}
