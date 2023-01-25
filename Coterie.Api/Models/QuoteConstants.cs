using System.Collections.Generic;

namespace Coterie.Api.Models
{
    public static class QuoteConstants
    {
        /// <summary>
        /// The states that a quote can be generated for, 
        /// each with their abbreviated name and the modifier applied to the premium for that state
        /// </summary>
        public static List<(string FullName, string AbbreviatedName, double Modifier)> States = new()
        {
            { ("OHIO", "OH", 1) },
            { ("FLORIDA", "FL", 1.2) },
            { ("TEXAS", "TX", 0.943) },
        };

        /// <summary>
        /// The types of businesses that a quote can be generated for and the
        /// modifier applied to the premium for each business
        /// </summary>
        public static List<(string BusinessName, double Modifier)> Businesses => new()
            {
                { ("ARCHITECT", 1) },
                { ("PLUMBER", 0.5) },
                { ("PROGRAMMER", 1.25) }
            };
    }
}
