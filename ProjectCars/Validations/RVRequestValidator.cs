using FluentValidation;
using ProjectCars.Models.Vehicle;

namespace ProjectCars.Validators
{
    public class RVRequestValidator : AbstractValidator<RVRequest>
    {
        public RVRequestValidator()
        {
            RuleFor(x => x.RVid).GreaterThan(0);
            RuleFor(x => x.RVBrand).MaximumLength(25);
            RuleFor(x => x.RVBrand).MinimumLength(5);
            RuleFor(x => x.RVModel).MaximumLength(25);
            RuleFor(x => x.RVModel).MaximumLength(5);
            RuleFor(x => x.RVColor).MaximumLength(25);
            RuleFor(x => x.RVColor).MinimumLength(5);
            RuleFor(x => x.Engine).MaximumLength(25);
            RuleFor(x => x.Engine).MinimumLength(5);
            RuleFor(x => x.Fuel).MaximumLength(25);
            RuleFor(x => x.Fuel).MinimumLength(5);
        }
    }
}
