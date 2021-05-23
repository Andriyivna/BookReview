using API.Entities;
using API.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace API.Data
{
    public class QuotesRepository : IQuotesRepository
    {
        private readonly AppDbContext _context;

        public QuotesRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Quote> GetQuoteOfTheDay()
        {
            return await _context.Quotes.Include(x => x.Book)
                .Include(x => x.Author)
                .FirstOrDefaultAsync(x => x.IsQuoteOfTheDay);
        }

        public async Task<Quote> SetNewQuoteOfTheDay(Quote prevQuote)
        {
            if (prevQuote != null)
            {
                prevQuote.IsQuoteOfTheDay = false;
            }

            var newQuoteOfTheDay = await GetNewQuoteOfTheDay();
            newQuoteOfTheDay.IsQuoteOfTheDay = true;
            newQuoteOfTheDay.QuoteOfTheDayDuration = DateTime.UtcNow.AddDays(1);

            await _context.SaveChangesAsync();

            return newQuoteOfTheDay;
        }

        private async Task<Quote> GetNewQuoteOfTheDay()
        {
            return await _context.Quotes.Include(x => x.Book)
                .Include(x => x.Author)
                .Where(x => x.QuoteOfTheDayDuration < DateTime.UtcNow.AddDays(-3))
                .OrderBy(x => Guid.NewGuid())
                .FirstOrDefaultAsync();
        }
    }
}
