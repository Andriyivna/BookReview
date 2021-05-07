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
        public double AverangeRates { get; set; }
        public string Description { get; set; }
        public int AuthorId { get; set; }
        public int GenreId { get; set; }
    }
}
