using First_App.Server.Models.DTOs;
using MediatR;

namespace First_App.Server.Models.RequestModels.TaskList
{
    public class GetAllTaskListsQuery : IRequest<List<TaskListDto>>
    {
        public Guid BoardId { get; set; }
    }
}
