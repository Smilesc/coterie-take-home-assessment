using System.Collections.Generic;

namespace Coterie.Api.Models.Requests
{
    public class QuoteRequest
    {
        /// <summary>
        /// The type of business
        /// </summary>
        /// <remarks>
        /// See <see cref="QuoteConstants.Businesses"/> for acceptable values
        /// </remarks>
        public string Business { get; set; }

        /// <summary>
        /// The business's revenue
        /// </summary>
        public double Revenue { get; set; }

        /// <summary>
        /// The states to generate quotes for
        /// </summary>
        /// <remarks>
        /// See <see cref="QuoteConstants.States"/> for acceptable values
        /// </remarks>
        public List<string> States { get; set; }
    }
}
