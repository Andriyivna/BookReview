using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Dtos
{
    public class BetterBook
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string CoverImg { get; set; }
        public string Publisher { get; set; }
        public int ReleaseYear { get; set; }
        public double AverageRates { get; set; }
        public string Description { get; set; }
        public string Author { get; set; }
        public string Genre { get; set; }
        public int AuthorId { get; set; }
        public int GenreId { get; set; }
    }
}
