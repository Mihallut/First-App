using First_App.Server.Models.DTOs;
using MediatR;
using System.Text.Json.Serialization;

namespace First_App.Server.Models.RequestModels.TaskList
{
    public class EditTaskListCommand : IRequest<TaskListDto>
    {
        [JsonIgnore]
        public Guid Id { get; set; }
        public string NewName { get; set; }
        public Guid BoardId { get; set; }
    }
}
