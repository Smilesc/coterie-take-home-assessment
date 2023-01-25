using Coterie.Api.Interfaces;
using Coterie.Api.Models.Requests;
using Coterie.Api.Models.Responses;
using Microsoft.AspNetCore.Mvc;

namespace Coterie.Api.Controllers
{
    [ApiController]
    [Route("quote")]
    public class QuoteController : Controller
    {
        private readonly IQuoteOrchestrator _quoteOrchestrator;
        
        public QuoteController(IQuoteOrchestrator quoteOrchestrator)
        {
            _quoteOrchestrator = quoteOrchestrator;
        }

        /// <summary>
        /// Generate per-state insurance quotes for a business
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult<QuoteResponse> GenerateQuote(QuoteRequest request)
        {
            return _quoteOrchestrator.GenerateQuote(request);
        }
    }
}