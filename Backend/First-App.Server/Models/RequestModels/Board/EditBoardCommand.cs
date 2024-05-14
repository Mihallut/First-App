using First_App.Server.Models.DTOs;
using MediatR;
using System.Text.Json.Serialization;

namespace First_App.Server.Models.RequestModels.Board
{
    public class EditBoardCommand : IRequest<BoardDto>
    {
        [JsonIgnore]
        public Guid Id { get; set; }
        public string Name { get; set; }
    }
}
