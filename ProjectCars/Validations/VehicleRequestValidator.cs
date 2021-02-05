using FluentValidation;
using ProjectCars.Models.Vehicle;

namespace ProjectCars.Validators
{
    public class VehicleRequestValidator : AbstractValidator<VehicleRequest>
    {
        public VehicleRequestValidator()
        {
            RuleFor(x => x.VehicleId).GreaterThan(0);
            RuleFor(x => x.VehicleBrand).MaximumLength(25);
            RuleFor(x => x.VehicleBrand).MinimumLength(5);
            RuleFor(x => x.VehicleModel).MaximumLength(25);
            RuleFor(x => x.VehicleModel).MaximumLength(5);
            RuleFor(x => x.VehicleType).MaximumLength(25);
            RuleFor(x => x.VehicleType).MinimumLength(5);
            RuleFor(x => x.VehicleColor).MaximumLength(25);
            RuleFor(x => x.VehicleColor).MinimumLength(5);
            RuleFor(x => x.Engine).MaximumLength(25);
            RuleFor(x => x.Engine).MinimumLength(5);
            RuleFor(x => x.Fuel).MaximumLength(25);
            RuleFor(x => x.Fuel).MinimumLength(5);
        }
    }
}
