using System.Collections.Generic;
using Coterie.Api.Models.Requests;
using Coterie.Api.Models.Responses;

namespace Coterie.Api.Interfaces
{
    public interface IQuoteOrchestrator
    {
        QuoteResponse GenerateQuote(QuoteRequest request);
    }
}