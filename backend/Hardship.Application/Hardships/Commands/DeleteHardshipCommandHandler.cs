using Hardship.Application.Abstractions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hardship.Application.Hardships.Commands
{
    // Handles deletion of an existing HardshipApplication.
    public class DeleteHardshipHandler : IRequestHandler<DeleteHardshipCommand, bool>
    {
        private readonly IApplicationDbContext _context;

        public DeleteHardshipHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<bool> Handle(DeleteHardshipCommand request, CancellationToken ct)
        {
            var hardshipApplication = await _context.Applications
                .FirstOrDefaultAsync(x => x.Id == request.Id, ct);

            if (hardshipApplication is null)
            {
                return false;
            }

            _context.Applications.Remove(hardshipApplication);
            await _context.SaveChangesAsync(ct);

            return true;
        }
    }
}
