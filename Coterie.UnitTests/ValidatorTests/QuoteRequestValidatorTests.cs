using Coterie.Api.Models.Requests;
using Coterie.Api.Models.Validators;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Coterie.UnitTests.ValidatorTests
{
    public class QuoteRequestValidatorTests
    {
        private readonly QuoteRequestValidator _quoteRequestValidator;
        public QuoteRequestValidatorTests() 
        {
            _quoteRequestValidator = new QuoteRequestValidator();
        }

        [Theory]
        [InlineData(new[] { "OH", "Florida", "TEXAS" }, "Plumber")]
        [InlineData(new[] { "FL" }, "architect")]
        [InlineData(new[] { "TX", "Ohio" }, "Programmer")]
        public void SupportedStatesAndBusiness_Suceeds(string[] states, string business)
        {
            // Arrange
            var request = new QuoteRequest
            {
                States = states.ToList(),
                Business = business,
                Revenue = 2000000
            };

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
