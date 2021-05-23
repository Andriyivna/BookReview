using API.Entities;
using System.Threading.Tasks;

namespace API.Interfaces
{
    public interface IQuotesRepository
    {
        Task<Quote> GetQuoteOfTheDay();
        Task<Quote> SetNewQuoteOfTheDay(Quote prevQuote);
    }
}
