using Coterie.Api.Models.Requests;
using Xunit;
using System.Collections.Generic;
using System.Threading.Tasks;
using Coterie.Api.Interfaces;
using Coterie.Api.Models;
using Coterie.Api.Repositories;
using Coterie.Api.Services;
using Microsoft.Extensions.Caching.Memory;
using NSubstitute;

namespace Coterie.UnitTests
{
    public class QuoteOrchestratorTests
    {
        private readonly IQuoteOrchestrator _quoteOrchestrator;

        private readonly IQuoteRepository _quoteRepository;
        private readonly IMemoryCache _memoryCache;
        
        public QuoteOrchestratorTests()
        {
            _quoteRepository = Substitute.For<IQuoteRepository>();
            _memoryCache = Substitute.For<IMemoryCache>();
            _quoteOrchestrator = new QuoteOrchestrator(_quoteRepository, _memoryCache);
        }

        [Theory]
        [InlineData("Ohio", "Plumber", 0.5, 1000, "OH")]
        [InlineData("Florida", "Architect", 1, 2400, "FL")]
        [InlineData("Texas", "Programmer", 1.25, 2357.50, "TX")]
        public void BasedOnStateAndBusiness_ReturnsExpectedPremium(string state, string business, decimal modifier, decimal expectedPremium, string expectedState)
        {
            // Arrange
            const decimal revenue = 500000;

            var request = new QuoteRequest 
            { 
                Business = business,
                States = new List<string> { state },
                Revenue = revenue
            };

            _quoteRepository.GetBusiness(business).Returns(new Business { Id = business, Modifier = modifier });
                
            // Act
            var result = _quoteOrchestrator.GenerateQuote(request);

            // Assert
            Assert.Equal(revenue, result.Revenue);
            Assert.Equal(business, result.Business);
            
            Assert.Single(result.Premiums);

            Assert.True(result.Premiums.Exists(x => x.State == expectedState && x.Premium == expectedPremium));
        }

        [Fact]
        public void MultipleStates_ReturnsPremiumPerState()
        {
            // Arrange
            const decimal revenue = 400000;
            const string business = "Programmer";
            const decimal businessModifier = 1.25m;

            var request = new QuoteRequest 
            { 
                Business = business,
                States = new List<string> { "Ohio", "FL", "tx" },
                Revenue = revenue
            };
            _quoteRepository.GetBusiness(business).Returns(new Business { Id = business, Modifier = businessModifier });

            // Act
            var result = _quoteOrchestrator.GenerateQuote(request);

            // Assert
            Assert.Equal(revenue, result.Revenue);
            Assert.Equal(business, result.Business);
            Assert.Equal(3, result.Premiums.Count);

            Assert.True(result.Premiums.Exists(x => x.State == "OH" && x.Premium == 2000));
            Assert.True(result.Premiums.Exists(x => x.State == "FL" && x.Premium == 2400));
            Assert.True(result.Premiums.Exists(x => x.State == "TX" && x.Premium == 1886));
        }
    }
}
