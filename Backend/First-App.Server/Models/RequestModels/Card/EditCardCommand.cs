using First_App.Server.Models.DTOs;
using MediatR;
using System.Text.Json.Serialization;

namespace First_App.Server.Models.RequestModels.Card
{
    public class EditCardCommand : IRequest<CardDto>
    {
        [JsonIgnore]
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateOnly DueDate { get; set; }
        public Guid TaskListId { get; set; }
        public uint PriorityId { get; set; }
    }
}
