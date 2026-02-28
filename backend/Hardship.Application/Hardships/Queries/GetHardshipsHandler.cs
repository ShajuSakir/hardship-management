using Hardship.Application.Abstractions;
using Hardship.Application.Hardships.DTOs;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Hardship.Application.Hardships.Queries
{
    public class GetHardshipsHandler
     : IRequestHandler<GetHardshipsQuery, List<HardshipDto>>
    {
        private readonly IApplicationDbContext _context;

        public GetHardshipsHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<HardshipDto>> Handle(
            GetHardshipsQuery request,
            CancellationToken ct)
        {
            return await _context.Applications
                .Select(x => new HardshipDto(
                    x.Id,
                    x.CustomerName,
                    x.DateOfBirth,
                    x.Income,
                    x.Expenses,
                    x.HardshipReason))
                .ToListAsync(ct);
        }
    }
}
