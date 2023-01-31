using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Coterie.Api.Interfaces;
using Coterie.Api.Models;
using Coterie.Api.Models.Requests;
using Coterie.Api.Models.Responses;
using Coterie.Api.Repositories;
using Microsoft.Extensions.Caching.Memory;

namespace Coterie.Api.Services
{
    public class QuoteOrchestrator : IQuoteOrchestrator
    {
        private readonly IQuoteRepository _quoteRepository;
        private readonly IMemoryCache _memoryCache;
        
        private const int HazardFactor = 4;

        public QuoteOrchestrator(IQuoteRepository quoteRepository, IMemoryCache memoryCache)
        {
            _quoteRepository = quoteRepository;
            _memoryCache = memoryCache;
        }
        
        public QuoteResponse GenerateQuote(QuoteRequest request)
        {
            var basePremium = Math.Ceiling(request.Revenue/1000);
            var business = GetBusiness(request.Business);

            var premiumModifier = basePremium * business.Modifier * HazardFactor;

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
                        Premium = decimal.Round(premium,2), 
                        State = stateInfo.AbbreviatedName
                    });
            }

            return quoteResponse;
        }

        public StateResponse GetStates()
        {
            var stateKey = "states";

            if (_memoryCache.TryGetValue(stateKey, out IEnumerable<State> cachedStates))
                return new StateResponse { States = cachedStates };
            
            var states = _quoteRepository.GetStates().ToList();
            
            _memoryCache.Set("states", states, TimeSpan.FromHours(6));
            
            return new StateResponse
            {
                States = states
            };
        }
        
        public Business GetBusiness(string requestedBusiness)
        {
            var key = $"business:{requestedBusiness.ToUpper()}";

            if (_memoryCache.TryGetValue(key, out Business cachedBusiness))
                return cachedBusiness;

            var business = _quoteRepository.GetBusiness(requestedBusiness);
            
            _memoryCache.Set($"business:{requestedBusiness.ToUpper()}", business, TimeSpan.FromHours(6));

            return business;
        }
    }
}
