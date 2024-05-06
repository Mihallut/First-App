using First_App.Server.Models.DTOs;
using First_App.Server.Models.RequestModels.Card;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace First_App.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CardController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CardController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("add")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<CardDto>> AddTaskList([FromBody] CreateCardCommand command, CancellationToken cancellationToken)
        {
            var card = await _mediator.Send(command, cancellationToken);
            return Ok(card);
        }

        [HttpPatch("edit/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<CardDto>> EditCard(Guid id, [FromBody] EditCardCommand command, CancellationToken cancellationToken)
        {
            command.Id = id;
            var card = await _mediator.Send(command, cancellationToken);
            return Ok(card);
        }

        [HttpDelete("delete/{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<CardDto>> DeleteCard(Guid id, CancellationToken cancellationToken)
        {
            await _mediator.Send(new DeleteCardCommand { Id = id }, cancellationToken);
            return NoContent();
        }
    }
}
