using System;
using System.Collections.Generic;

namespace Coterie.Api.Models.Responses
{
    public class QuoteResponse : BaseSuccessResponse
    {
        public string Business { get; set; }
        public double Revenue { get; set; }
        public List<StatePremium> Premiums { get; set; }
    }
}
