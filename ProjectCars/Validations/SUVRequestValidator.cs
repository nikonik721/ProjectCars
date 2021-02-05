using FluentValidation;
using ProjectCars.Models.Vehicle;

namespace ProjectCars.Validators
{
    public class SUVRequestValidator : AbstractValidator<SUVRequest>
    {
        public SUVRequestValidator()
        {
            RuleFor(x => x.SUVId).GreaterThan(0);
            RuleFor(x => x.SUVBrand).MaximumLength(25);
            RuleFor(x => x.SUVBrand).MinimumLength(5);
            RuleFor(x => x.SUVModel).MaximumLength(25);
            RuleFor(x => x.SUVModel).MaximumLength(5);
            RuleFor(x => x.SUVType).MaximumLength(25);
            RuleFor(x => x.SUVType).MinimumLength(5);
            RuleFor(x => x.Engine).MaximumLength(25);
            RuleFor(x => x.Engine).MinimumLength(5);
            RuleFor(x => x.Fuel).MaximumLength(25);
            RuleFor(x => x.Fuel).MinimumLength(5);
        }
    }
}
