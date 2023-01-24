using Coterie.Api.Models.Requests;
using FluentValidation;
using System.Collections.Generic;

namespace Coterie.Api.NewFolder
{
    public class QuoteRequestValidator : AbstractValidator<QuoteRequest>
    {
        public QuoteRequestValidator()
        {
            var allowedStateAbbrvs = new List<string> { "tx", "fl", "oh" };
            var allowedStates = new List<string> { "texas", "florida", "ohio" };
            var allowedBusinesses = new List<string> { "Architect", "Plumber", "Programmer " };

            RuleFor(x => x.States).Must(states => states.TrueForAll(state => allowedStateAbbrvs.Contains(state) || allowedStates.Contains(state.ToLower()))).WithMessage("Sorry, at least one of the states entered aren't supported");
            RuleFor(x => x.Business).Must(business => allowedBusinesses.Contains(business)).WithMessage("Sorry, that business isn't supported");
        }
    }
}
