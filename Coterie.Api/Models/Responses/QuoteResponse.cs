using System;
using System.Collections.Generic;

namespace Coterie.Api.Models.Responses
{
    public class QuoteResponse
    {
        public string Business { get; set; }
        public double Revenue { get; set; }
        public List<StatePremium> Premiums { get; set; }
        public bool IsSuccessful { get; set; }
        public string TransactionId { get; set; } = Guid.NewGuid().ToString();

    }
}
