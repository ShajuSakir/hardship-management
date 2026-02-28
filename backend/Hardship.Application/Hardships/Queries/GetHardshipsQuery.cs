using Hardship.Application.Hardships.DTOs;
using MediatR;

namespace Hardship.Application.Hardships.Queries
{
    public record GetHardshipsQuery() : IRequest<List<HardshipDto>>;
}
