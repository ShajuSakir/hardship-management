using Hardship.Application.Abstractions;
using Hardship.Application.Common.Exceptions;
using Hardship.Application.Hardships.DTOs;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Hardship.Application.Hardships.Queries
{
    public class GetHardshipByIdHandler
         : IRequestHandler<GetHardshipByIdQuery, HardshipDto?>
    {
        private readonly IApplicationDbContext _context;

        public GetHardshipByIdHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<HardshipDto?> Handle(
            GetHardshipByIdQuery request,
            CancellationToken ct)
        {
            var result = await _context.Applications
                .Where(x => x.Id == request.Id)
                .Select(x => new HardshipDto(
                    x.Id,
                    x.CustomerName,
                    x.DateOfBirth,
                    x.Income,
                    x.Expenses,
                    x.HardshipReason))
                .FirstOrDefaultAsync(ct);

            if (result is null)
                throw new NotFoundException("Hardship application not found.");

            return result;
        }
    }
}
