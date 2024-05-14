using First_App.Server.Models.DTOs;
using MediatR;

namespace First_App.Server.Models.RequestModels.Board
{
    public class CreateBoardCommand : IRequest<BoardDto>
    {
        public string Name { get; set; }
    }
}