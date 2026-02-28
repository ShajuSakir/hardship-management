using MediatR;

namespace Hardship.Application.Hardships.Commands
{
    public record CreateHardshipCommand(
     string CustomerName,
     DateTime DateOfBirth,
     decimal Income,
     decimal Expenses,
     string? HardshipReason
 ) : IRequest<Guid>;
}
