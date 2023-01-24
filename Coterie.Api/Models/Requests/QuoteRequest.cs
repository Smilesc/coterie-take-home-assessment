using System.Collections.Generic;

namespace Coterie.Api.Models.Requests
{
    public class QuoteRequest
    {
        public string Business { get; set; }
        public double Revenue { get; set; }
        public List<string> States { get; set; }
    }
}
