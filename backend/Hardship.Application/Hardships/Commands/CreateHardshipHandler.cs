using Hardship.Application.Abstractions;
using Hardship.Domain.Entities;
using MediatR;

namespace Hardship.Application.Hardships.Commands
{
    // Handles the creation of a new HardshipApplication.
    // Business validation is handled by FluentValidation pipeline.
    public class CreateHardshipHandler
     : IRequestHandler<CreateHardshipCommand, Guid>
    {
        private readonly IApplicationDbContext _context;

        public CreateHardshipHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Guid> Handle(CreateHardshipCommand request, CancellationToken ct)
        {
            var hardshipApplication = new HardshipApplication(
                request.CustomerName,
                request.DateOfBirth,
                request.Income,
                request.Expenses,
                request.HardshipReason);

            _context.Applications.Add(hardshipApplication);
            await _context.SaveChangesAsync(ct);

            return hardshipApplication.Id;
        }
    }
}
