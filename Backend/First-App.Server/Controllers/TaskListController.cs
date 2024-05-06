using First_App.Server.Models.DTOs;
using First_App.Server.Models.RequestModels.TaskList;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace First_App.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaskListController : ControllerBase
    {
        private readonly IMediator _mediator;

        public TaskListController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<TaskListDto>> GetAllTaskLists(CancellationToken cancellationToken)
        {
            var taskList = await _mediator.Send(new GetAllTaskListsQuery(), cancellationToken);
            return Ok(taskList);
        }

        [HttpPost("add")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<TaskListDto>> AddTaskList([FromBody] CreateTaskListCommand command, CancellationToken cancellationToken)
        {
            var taskList = await _mediator.Send(command, cancellationToken);
            return Ok(taskList);
        }

        [HttpPatch("edit/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<TaskListDto>> EditTaskList(Guid id, [FromBody] EditTaskListCommand command, CancellationToken cancellationToken)
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
        public async Task<ActionResult<TaskListDto>> DeleteTaskList(Guid id, CancellationToken cancellationToken)
        {
            await _mediator.Send(new DeleteTaskListCommand { Id = id }, cancellationToken);
            return NoContent();
        }
    }
}
