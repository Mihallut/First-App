using First_App.Server.Models.DTOs;
using MediatR;

namespace First_App.Server.Models.RequestModels.TaskList
{
    public class CreateTaskListCommand : IRequest<TaskListDto>
    {
        public string Name { get; set; }
        public Guid BoardId { get; set; }
    }
}
