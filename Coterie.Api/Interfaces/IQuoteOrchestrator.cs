using System.Collections.Generic;
using System.Threading.Tasks;
using Coterie.Api.Models;
using Coterie.Api.Models.Requests;
using Coterie.Api.Models.Responses;

namespace Coterie.Api.Interfaces
{
    public interface IQuoteOrchestrator
    {
        QuoteResponse GenerateQuote(QuoteRequest request);
        StateResponse GetStates();
        Business GetBusiness(string requestedBusiness);
    }
}
