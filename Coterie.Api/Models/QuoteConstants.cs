using System.Collections.Generic;

namespace Coterie.Api.Models
{
    public static class QuoteConstants
    {
        /// <summary>
        /// The states that a quote can be generated for, 
        /// each with their abbreviated name and the modifier applied to the premium for that state
        /// </summary>
        public static List<(string FullName, string AbbreviatedName, decimal Modifier)> States = new()
        {
            { ("OHIO", "OH", 1) },
            { ("FLORIDA", "FL", 1.2m) },
            { ("TEXAS", "TX", 0.943m) },
        };

        /// <summary>
        /// The types of businesses that a quote can be generated for and the
        /// modifier applied to the premium for each business
        /// </summary>
        public static List<(string BusinessName, decimal Modifier)> Businesses => new()
            {
                { ("ARCHITECT", 1) },
                { ("PLUMBER", 0.5m) },
                { ("PROGRAMMER", 1.25m) }
            };
    }
}
