using API.Dtos;
using API.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuotesController : ControllerBase
    {
        private readonly IQuotesRepository _repo;
        private readonly IMapper _mapper;

        public QuotesController(IQuotesRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        [HttpGet]
        [Route("/quotes/quoteoftheday")]
        public async Task<ActionResult> GetQuoteOfTheDay()
        {
            var quoteOfTheDay = await _repo.GetQuoteOfTheDay();

            if (quoteOfTheDay == null || quoteOfTheDay.QuoteOfTheDayDuration < DateTime.UtcNow)
            {
                quoteOfTheDay = await _repo.SetNewQuoteOfTheDay(quoteOfTheDay);
            }

            var quoteToReturn = _mapper.Map<QuoteOfTheDayDto>(quoteOfTheDay);

            return Ok(quoteToReturn);
        }
    }
}
