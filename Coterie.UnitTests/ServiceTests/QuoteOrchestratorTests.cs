using Coterie.Api.Models.Requests;
using Xunit;
using System.Collections.Generic;
using Coterie.Api.Interfaces;
using Coterie.Api.Services;

namespace Coterie.UnitTests
{
    public class QuoteOrchestratorTests
    {
        private readonly IQuoteOrchestrator _quoteOrchestrator;
        public QuoteOrchestratorTests() 
        {
            var quoteOrchestrator = new QuoteOrchestrator();
            _quoteOrchestrator = quoteOrchestrator;
        }

        [Theory]
        [InlineData("Ohio", "Plumber", 1000, "OH")]
        [InlineData("Florida", "Architect", 2400, "FL")]
        [InlineData("Texas", "Programmer", 2357.50, "TX")]
        public void BasedOnStateAndBusiness_ReturnsExpectedPremium(string state, string business, double expectedPremium, string expectedState)
        {
            // Arrange
            const double revenue = 500000;

            var request = new QuoteRequest 
            { 
                Business = business,
                States = new List<string> { state },
                Revenue = revenue
            };

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
            const double revenue = 400000;
            const string business = "Programmer";

            var request = new QuoteRequest 
            { 
                Business = business,
                States = new List<string> { "Ohio", "FL", "tx" },
                Revenue = revenue
            };

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