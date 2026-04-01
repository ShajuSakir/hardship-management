using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hardship.Application.Hardships.Commands
{
    // This command carries the id of the hardship record to delete.
    public record DeleteHardshipCommand(Guid Id) : IRequest<bool>;
}
