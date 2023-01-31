using Coterie.Api.Models.Requests;
using Coterie.Api.Models.Validators;
using System.Collections.Generic;
using System.Linq;
using Coterie.Api.Interfaces;
using Coterie.Api.Models;
using NSubstitute;
using NSubstitute.ReturnsExtensions;
using Xunit;

namespace Coterie.UnitTests.ValidatorTests
{
    public class QuoteRequestValidatorTests
    {
        private readonly QuoteRequestValidator _quoteRequestValidator;
        private readonly IQuoteOrchestrator _quoteOrchestrator;
        
        public QuoteRequestValidatorTests()
        {
            _quoteOrchestrator = Substitute.For<IQuoteOrchestrator>();
            
            _quoteRequestValidator = new QuoteRequestValidator(_quoteOrchestrator);
        }

        [Theory]
        [InlineData(new[] { "OH", "Florida", "TEXAS" }, "Plumber",  0.5)]
        [InlineData(new[] { "FL" }, "architect", 1)]
        [InlineData(new[] { "TX", "Ohio" }, "Programmer", 1.25)]
        public void SupportedStatesAndBusiness_Succeeds(string[] states, string business, decimal businessModifier)
        {
            // Arrange
            var request = new QuoteRequest
            {
                States = states.ToList(),
                Business = business,
                Revenue = 2000000
            };
            
            _quoteOrchestrator.GetBusiness(business).Returns(new Business { Id = business, Modifier = businessModifier });


            // Act
            var result = _quoteRequestValidator.Validate(request);

            // Assert
            Assert.True(result.IsValid);
        }

        [Theory]
        [InlineData(new[] { "OH", "Florida", "New York" }, "Plumber", new[] { "UNSUP_STATE"})]
        [InlineData(new[] { "FL" }, "chef", new[] { "UNSUP_BUSINESS"})]
        [InlineData(new[] { "NY", "Texas" }, "Lawyer", new[] { "UNSUP_STATE", "UNSUP_BUSINESS" })]
        public void UnsupportedStateOrBusiness_DoesNotSuceed(string[] states, string business, string[] expectedErrorCodes)
        {
            // Arrange
            var request = new QuoteRequest
            {
                States = states.ToList(),
                Business = business,
                Revenue = 2000000
            };
            
            _quoteOrchestrator.GetBusiness("Plumber").Returns(new Business { Id = business, Modifier = 0.5m });
            _quoteOrchestrator.GetBusiness(Arg.Is<string>(x => x != "Plumber")).ReturnsNull();
            
            // Act
            var result = _quoteRequestValidator.Validate(request);

            // Assert
            Assert.False(result.IsValid);
            Assert.True(result.Errors.Select(x=> x.ErrorCode).SequenceEqual(expectedErrorCodes.ToList()));
        }

        [Fact]
        public void NoRevenue_DoesNotSuceed()
        {
            // Arrange
            var request = new QuoteRequest
            {
                States = new List<string> { "OH", "Texas" },
                Business = "Programmer"
            };

            // Act
            var result = _quoteRequestValidator.Validate(request);

            // Assert
            Assert.False(result.IsValid);
        }
    }
}
