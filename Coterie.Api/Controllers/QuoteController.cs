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

        [HttpPost]
        public ActionResult<QuoteResponse> Get(QuoteRequest request)
        {
            return _quoteOrchestrator.GetQuote(request);
        }
    }
}