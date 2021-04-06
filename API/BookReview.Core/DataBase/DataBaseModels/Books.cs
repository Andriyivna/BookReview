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
        public string Author { get; set; }
        public string Printer { get; set; }
        public string Description { get; set; }
        public List<MyShelf> MyShelfs { get; set; }
        public List<Review> Reviews { get; set; }

        public Books()
        {

        }
        public Books(string title, string author, string printer, string description)
        {
            SetTitle(title);
            SetAuthor(author);
            SetPrinter(printer);
            SetDesc(description);
        }

        public void SetTitle(string title)
        {
            if (string.IsNullOrWhiteSpace(title))
            {
                throw new ApplicationException("Title of book can not be empty");
            }
            Title = title;
        }
        public void SetAuthor(string author)
        {
            if (string.IsNullOrWhiteSpace(author))
            {
                throw new ApplicationException("Author of book can not be empty");
            }
            Author = author;
        }
        public void SetPrinter(string printer)
        {
            if (string.IsNullOrWhiteSpace(printer))
            {
                throw new ApplicationException("Printer of book can not be empty");
            }
            Printer = printer;
        }
        public void SetDesc(string desc)
        {
            if (string.IsNullOrWhiteSpace(desc))
            {
                throw new ApplicationException("Descriptions of book can not be empty");
            }
            Description = desc;
        }
    }
}
