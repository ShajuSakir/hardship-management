using Hardship.Application.Hardships.Commands;
using Hardship.Application.Hardships.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Hardship.API.Controllers
{
    [Route("api/hardships")]
    [ApiController]
    public class HardshipsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public HardshipsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateHardshipCommand command, CancellationToken ct)
        {
            var id = await _mediator.Send(command, ct);
            return CreatedAtAction(nameof(GetAll), new { id }, id);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _mediator.Send(new GetHardshipsQuery());
            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, UpdateHardshipCommand command, CancellationToken ct)
        {
            command = command with { Id = id };

            await _mediator.Send(command, ct);
            return NoContent();
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var result = await _mediator.Send(new GetHardshipByIdQuery(id));

            if (result == null)
                return NotFound(new
                {
                    title = "Not Found",
                    status = 404,
                    detail = $"Hardship with ID {id} not found"
                });

            return Ok(result);
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete(Guid id, CancellationToken ct)
        {
            var deleted = await _mediator.Send(new DeleteHardshipCommand(id), ct);
            if (!deleted)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}
