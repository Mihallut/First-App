using First_App.Server.Helpers.Interfases;
using First_App.Server.Models.RequestModels.TaskList;
using First_App.Server.Repositories.Interfaces;
using MediatR;

namespace First_App.Server.Handlers.TaskLists
{
    public class DeleteTaskListCommandHandler : IRequestHandler<DeleteTaskListCommand>
    {
        private readonly ITaskListRepository _taskListRepository;
        private readonly ICardRepository _cardRepository;
        private readonly IActivityLogGenerator _activityLogGenerator;

        public DeleteTaskListCommandHandler(ITaskListRepository taskListRepository, ICardRepository cardRepository, IActivityLogGenerator activityLogGenerator)
        {
            _taskListRepository = taskListRepository;
            _cardRepository = cardRepository;
            _activityLogGenerator = activityLogGenerator;
        }

        public async Task Handle(DeleteTaskListCommand request, CancellationToken cancellationToken)
        {
            var taskList = await _taskListRepository.GetTaskListById(request.Id);
            foreach (var card in taskList.Cards.ToList())
            {
                await _activityLogGenerator.GenerateDeleteLog(card);
                await _cardRepository.DeleteCard(card.Id);
            }
            await _taskListRepository.DeleteTaskList(request.Id);
        }
    }
}
