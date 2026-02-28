using MediatR;

namespace Hardship.Application.Hardships.Commands
{
    public record UpdateHardshipCommand(
     Guid Id,
     string CustomerName,
     DateTime DateOfBirth,
     decimal Income,
     decimal Expenses,
     string? HardshipReason
 ) : IRequest;
}
