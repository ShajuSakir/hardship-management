using FluentValidation;

namespace Hardship.Application.Hardships.Commands
{
    public class CreateHardshipValidator
    : AbstractValidator<CreateHardshipCommand>
    {
        public CreateHardshipValidator()
        {
            RuleFor(x => x.CustomerName).NotEmpty();
            RuleFor(x => x.DateOfBirth).LessThan(DateTime.UtcNow);
            RuleFor(x => x.Income).GreaterThanOrEqualTo(0);
            RuleFor(x => x.Expenses).GreaterThanOrEqualTo(0);
        }
    }
}
