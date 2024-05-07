﻿using First_App.Server.Models.DTOs;
using First_App.Server.Models.RequestModels.Board;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace First_App.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BoardController : ControllerBase
    {
        private readonly IMediator _mediator;

        public BoardController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<BoardDto>> GetAllBoards(CancellationToken cancellationToken)
        {
            var taskList = await _mediator.Send(new GetAllBoardsQuery(), cancellationToken);
            return Ok(taskList);
        }

        [HttpPost("add")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<BoardDto>> AddBoard([FromBody] CreateBoardCommand command, CancellationToken cancellationToken)
        {
            var taskList = await _mediator.Send(command, cancellationToken);
            return Ok(taskList);
        }

        [HttpPatch("edit/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<BoardDto>> EditBoard(Guid id, [FromBody] EditBoardCommand command, CancellationToken cancellationToken)
        {
            command.Id = id;
            var taskList = await _mediator.Send(command, cancellationToken);
            return Ok(taskList);
        }

        [HttpDelete("delete/{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<TaskListDto>> DeleteBoard(Guid id, CancellationToken cancellationToken)
        {
            await _mediator.Send(new DeleteBoardCommand { Id = id }, cancellationToken);
            return NoContent();
        }
    }
}