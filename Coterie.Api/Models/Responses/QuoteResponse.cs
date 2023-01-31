using System;
using System.Collections.Generic;

namespace Coterie.Api.Models.Responses
{
    public class QuoteResponse : BaseSuccessResponse
    {
        /// <summary>
        /// The input type of business 
        /// </summary>
        public string Business { get; set; }

        /// <summary>
        /// The input revenue
        /// </summary>
        public decimal Revenue { get; set; }

        /// <summary>
        /// The premiums calculated for each state
        /// </summary>
        public List<StatePremium> Premiums { get; set; }
    }
}
