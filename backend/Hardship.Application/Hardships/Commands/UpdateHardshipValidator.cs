using FluentValidation;

namespace Hardship.Application.Hardships.Commands
{
    public class UpdateHardshipValidator
     : AbstractValidator<UpdateHardshipCommand>
    {
        public UpdateHardshipValidator()
        {
            RuleFor(x => x.Id).NotEmpty();
            RuleFor(x => x.CustomerName).NotEmpty();
            RuleFor(x => x.DateOfBirth).LessThan(DateTime.UtcNow);
            RuleFor(x => x.Income).GreaterThanOrEqualTo(0);
            RuleFor(x => x.Expenses).GreaterThanOrEqualTo(0);
        }
    }
}
