using System.Collections.Generic;

namespace Coterie.Api.Models
{
    public static class QuoteConstants
    {
        public static List<(string FullName, string AbbreviatedName, double Modifier)> States = new()
        {
            { ("OHIO", "OH", 1) },
            { ("FLORIDA", "FL", 1.2) },
            { ("TEXAS", "TX", 0.943) },
        };

        public static List<(string BusinessName, double Modifier)> Businesses => new()
            {
                { ("ARCHITECT", 1) },
                { ("PLUMBER", 0.5) },
                { ("PROGRAMMER", 1.25) }
            };
    }
}
