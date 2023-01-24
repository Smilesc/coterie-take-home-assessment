using Coterie.Api.Models.Requests;
using FluentValidation;

namespace Coterie.Api.Models.Validators
{
    public class QuoteRequestValidator : AbstractValidator<QuoteRequest>
    {
        public QuoteRequestValidator()
        {
            RuleFor(x => x.Revenue)
                .NotEmpty()
                .WithMessage("Must include revenue");

            RuleFor(x => x.States)
            .Must(states => states.TrueForAll(state =>
                QuoteConstants.States.Exists(x => state.ToUpper() == x.FullName || state.ToUpper() == x.AbbreviatedName)))
            .WithMessage("At least one of the states given isn't supported");

            RuleFor(x => x.Business)
                .Must(business => QuoteConstants.Businesses.Exists(x => business.ToUpper() == x.BusinessName))
                .WithMessage("The business given isn't supported");
        }
    }
}
