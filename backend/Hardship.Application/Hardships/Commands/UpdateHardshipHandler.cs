using Hardship.Application.Abstractions;
using Hardship.Application.Common.Exceptions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Hardship.Application.Hardships.Commands
{
    public class UpdateHardshipHandler
    : IRequestHandler<UpdateHardshipCommand>
    {
        private readonly IApplicationDbContext _context;

        public UpdateHardshipHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task Handle(UpdateHardshipCommand request, CancellationToken ct)
        {
            var entity = await _context.Applications
                .FirstOrDefaultAsync(x => x.Id == request.Id, ct);

            if (entity == null)
                throw new NotFoundException("Hardship application not found.");

            entity.Update(
                request.CustomerName,
                request.DateOfBirth,
                request.Income,
                request.Expenses,
                request.HardshipReason);

            await _context.SaveChangesAsync(ct);
        }
    }
}
