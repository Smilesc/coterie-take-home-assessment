using System.Threading;
using System.Threading.Tasks;
using Coterie.Api.Interfaces;
using Coterie.Api.Models.Requests;
using FluentValidation;
using FluentValidation.Results;

namespace Coterie.Api.Models.Validators
{
    public class QuoteRequestValidator : AbstractValidator<QuoteRequest>
    {
        private readonly IQuoteOrchestrator _quoteOrchestrator;
        
        public QuoteRequestValidator(IQuoteOrchestrator quoteOrchestrator)
        {
            _quoteOrchestrator = quoteOrchestrator;
            
            RuleFor(x => x.Revenue)
                .NotEmpty()
                .WithMessage("Must include revenue");

            RuleFor(x => x.States)
            .Must(states => states.TrueForAll(state =>
                QuoteConstants.States.Exists(x => state.ToUpper() == x.FullName || state.ToUpper() == x.AbbreviatedName)))
            .WithMessage("At least one of the states given isn't supported").WithErrorCode("UNSUP_STATE");

            RuleFor(x => x.Business)
                .Custom(ValidateBusiness);
        }

        private void ValidateBusiness(string business, ValidationContext<QuoteRequest> context)
        {
            var result = _quoteOrchestrator.GetBusiness(business);
            if(result is null)
            {
                context.AddFailure(new ValidationFailure{ErrorCode = "UNSUP_BUSINESS", ErrorMessage = "The business given isn't supported"});
            }
        }
    }
}
