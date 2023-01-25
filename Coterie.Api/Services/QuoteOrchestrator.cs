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

        public QuoteResponse GenerateQuote(QuoteRequest request)
        {
            var basePremium = Math.Ceiling(request.Revenue/1000);
            var businessModifier = QuoteConstants.Businesses.First(x => x.BusinessName == request.Business.ToUpper()).Modifier;

            var premiumModifier = basePremium * businessModifier * HazardFactor;

            var quoteResponse = new QuoteResponse 
            { 
                Business = request.Business,
                Revenue = request.Revenue,
                Premiums = new List<StatePremium>()
            };

            foreach (var state in request.States.Select(x => x.ToUpper()))
            {
                var stateInfo = QuoteConstants.States.First(x => state == x.FullName || state == x.AbbreviatedName);
                
                var premium = stateInfo.Modifier * premiumModifier;

                quoteResponse.Premiums.Add(
                    new StatePremium 
                    {
                        Premium = premium, 
                        State = stateInfo.AbbreviatedName
                    });
            }

            return quoteResponse;
        }
    }
}
