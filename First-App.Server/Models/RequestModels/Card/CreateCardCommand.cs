using First_App.Server.Models.DTOs;
using MediatR;

namespace First_App.Server.Models.RequestModels.Card
{
    public class CreateCardCommand : IRequest<CardDto>
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public DateOnly DueDate { get; set; }
        public Guid TaskListId { get; set; }
        public uint PriorityId { get; set; }
    }
}
