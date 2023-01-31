namespace Coterie.Api.Models.Responses
{
    public class StatePremium
    {
        /// <summary>
        /// The calculated premium
        /// </summary>
        public decimal Premium { get; set; }

        /// <summary>
        /// The state the premium has been calculated for,
        /// represented as a two-letter abbreviation
        /// </summary>
        /// <example>TX</example>
        public string State { get; set; }
    }
}
