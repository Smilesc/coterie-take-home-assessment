using Coterie.Api.Controllers;
using Coterie.Api.Interfaces;
using Coterie.Api.Models.Requests;
using Coterie.Api.Models.Responses;
using NSubstitute;
using NSubstitute.ExceptionExtensions;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace Coterie.UnitTests.ControllerTests
{
    public class QuoteControllerTests
    {
        private readonly QuoteController _quoteController;
        private readonly IQuoteOrchestrator _quoteOrchestrator;

        public QuoteControllerTests()
        {
            _quoteOrchestrator = Substitute.For<IQuoteOrchestrator>();

            _quoteController = new QuoteController(_quoteOrchestrator);
        }

        [Fact]
        public void ReturnsExpectedResponse()
        {
            // Arrange
            var request = new QuoteRequest 
            { 
                Business = "Plumber",
                States = new List<string> { "OH" },
                Revenue = 6000000 
            };
            
            var response = new QuoteResponse 
            { 
                Business = "Plumber",
                Premiums = new List<StatePremium> 
                { 
                    new StatePremium { State = "OH", Premium = 12000 } 
                } 
            };

            _quoteOrchestrator.GenerateQuote(request).Returns(response);

            // Act
            var result = _quoteController.GenerateQuote(request);

            // Assert
            Assert.True(result.Value.IsSuccessful);
            Assert.Equal(response, result.Value);
        }
    }
}
