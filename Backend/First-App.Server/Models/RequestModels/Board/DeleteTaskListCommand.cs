using MediatR;

namespace First_App.Server.Models.RequestModels.Board
{
    public class DeleteBoardCommand : IRequest
    {
        public Guid Id { get; set; }
    }
}
