using First_App.Server.Models.DTOs;
using MediatR;

namespace First_App.Server.Models.RequestModels.Board
{
    public class GetAllBoardsQuery : IRequest<List<BoardDto>>
    {
    }
}
