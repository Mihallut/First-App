using MediatR;

namespace First_App.Server.Models.RequestModels.TaskList
{
    public class DeleteTaskListCommand : IRequest
    {
        public Guid Id { get; set; }
    }
}
