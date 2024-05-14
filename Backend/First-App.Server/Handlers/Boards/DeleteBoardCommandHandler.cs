using First_App.Server.Models.RequestModels.Board;
using First_App.Server.Repositories.Interfaces;
using MediatR;

namespace First_App.Server.Handlers.Board
{
    public class DeleteBoardCommandHandler : IRequestHandler<DeleteBoardCommand>
    {
        private readonly ITaskListRepository _taskListRepository;
        private readonly ICardRepository _cardRepository;
        private readonly IBoardRepository _boardRepository;
        public DeleteBoardCommandHandler(ITaskListRepository taskListRepository, ICardRepository cardRepository, IBoardRepository boardRepository)
        {
            _taskListRepository = taskListRepository;
            _cardRepository = cardRepository;
            _boardRepository = boardRepository;
        }

        public async Task Handle(DeleteBoardCommand request, CancellationToken cancellationToken)
        {
            var taskLists = await _taskListRepository.GetAllTaskLists(request.Id);
            foreach (var taskList in taskLists)
            {
                foreach (var card in taskList.Cards.ToList())
                {
                    await _cardRepository.DeleteCard(card.Id);
                }
                await _taskListRepository.DeleteTaskList(taskList.Id);
            }

            await _boardRepository.DeleteBoard(request.Id);
        }
    }
}
