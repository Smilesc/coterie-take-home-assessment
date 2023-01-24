using System;
using System.Collections.Generic;
using System.Linq;
using Coterie.Api.Interfaces;
using Coterie.Api.Models;
using Coterie.Api.Models.Requests;
using Coterie.Api.Models.Responses;

namespace Coterie.Api.Services
{
    public class QuoteOrchestrator : IQuoteOrchestrator
    {
        private const int HazardFactor = 4;
        public QuoteResponse GetQuote(QuoteRequest request)
        {
            var basePremium = Math.Ceiling(request.Revenue);
            var businessModifier = BusinessModifiers[request.Business.ToLower()];

            var premiumModifier = basePremium * businessModifier * HazardFactor;

            var quoteResponse = new QuoteResponse 
            { 
                Business = request.Business, 
                Revenue = request.Revenue, 
                Premiums = new List<StatePremium>()
            };

            foreach (var state in request.States)
            {
                var standardizedState = StandardizeState(state);

                var premium = StateModifiers[standardizedState] * premiumModifier;

                quoteResponse.Premiums.Add(new StatePremium { Premium = premium, State = standardizedState });
            }

            return quoteResponse;
        }

        private static string StandardizeState(string requestState)
        {
            return (requestState.ToLower()) switch
            {
                ("ohio" or "oh") => QuoteState.Ohio,
                ("florida" or "fl") => QuoteState.Florida,
                ("texas" or "tx") => QuoteState.Texas,
                _ => throw new ArgumentException("State is not supported")
            };
        }

        private static Dictionary<string, double> StateModifiers => new()
            {
                { QuoteState.Ohio, 1 },
                { QuoteState.Florida, 1.2 },
                { QuoteState.Texas, 0.94 }
            };

        private static Dictionary<string, double> BusinessModifiers => new()
            {
                { "architect", 1 },
                { "plumber", 0.5 },
                { "programmer", 1.25 }
            };
    }
}