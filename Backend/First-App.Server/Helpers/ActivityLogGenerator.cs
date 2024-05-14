using First_App.Server.Helpers.Interfases;
using First_App.Server.Models;
using First_App.Server.Repositories.Interfaces;

namespace First_App.Server.Helpers
{
    public class ActivityLogGenerator : IActivityLogGenerator
    {
        private readonly IActivityLogRepository _activityLogRepository;
        private readonly ITaskListRepository _taskListRepository;
        private readonly ICardRepository _cardRepository;
        public ActivityLogGenerator(IActivityLogRepository activityLogRepository, ITaskListRepository taskListRepository, ICardRepository cardRepository)
        {
            _activityLogRepository = activityLogRepository;
            _taskListRepository = taskListRepository;
            _cardRepository = cardRepository;
        }

        public async Task GenerateCreateLog(Card card)
        {
            ActivityLogType logType = await _activityLogRepository.GetActivityLogTypeByName("Create");
            if (logType == null)
            {
                throw new NullReferenceException("Provided activity log type name does not exist in database.");
            }
            var cardTaskList = await _taskListRepository.GetTaskListById(card.TaskListId);
            Models.ActivityLog activityLog = new Models.ActivityLog
            {
                Id = Guid.NewGuid(),
                ChangedCardId = card.Id,
                ChangedCardTitle = card.Title,
                CreationDate = DateTime.Now.ToUniversalTime(),
                ChangedFieldName = nameof(card.TaskList),
                ValueAfter = cardTaskList.Name,
                ActivityLogTypeId = logType.Id,
                BoardId = cardTaskList.BoardId
            };
            await _activityLogRepository.AddActivityLog(activityLog);
        }

        public async Task GenerateDeleteLog(Card card)
        {
            ActivityLogType logType = await _activityLogRepository.GetActivityLogTypeByName("Delete");
            if (logType == null)
            {
                throw new NullReferenceException("Provided activity log type name does not exist in database.");
            }
            var cardTaskList = await _taskListRepository.GetTaskListById(card.TaskListId);
            Models.ActivityLog activityLog = new Models.ActivityLog
            {
                Id = Guid.NewGuid(),
                ChangedCardId = card.Id,
                ChangedCardTitle = card.Title,
                ChangedFieldName = nameof(card.TaskList),
                ValueBefore = cardTaskList.Name,
                CreationDate = DateTime.Now.ToUniversalTime(),
                ActivityLogTypeId = logType.Id,
                BoardId = cardTaskList.BoardId
            };
            await _activityLogRepository.AddActivityLog(activityLog);
        }

        public async Task GenerateEditLog(Card cardBeforeEdit, Card editedCard)
        {
            if (cardBeforeEdit.Title != editedCard.Title)
            {
                await GenerateRenameLog(cardBeforeEdit, editedCard);
            }
            if (cardBeforeEdit.DueDate != editedCard.DueDate)
            {
                await GenerateCardEditDueDateLog(cardBeforeEdit, editedCard);
            }
            if (cardBeforeEdit.Description != editedCard.Description)
            {
                await GenerateEditDescriptionLog(cardBeforeEdit, editedCard);
            }
            if (cardBeforeEdit.TaskListId != editedCard.TaskListId)
            {
                await GenerateMoveLog(cardBeforeEdit, editedCard);
            }
            if (cardBeforeEdit.PriorityId != editedCard.PriorityId)
            {
                await GenerateEditPriorityLog(cardBeforeEdit, editedCard);
            }
        }

        private async Task GenerateEditPriorityLog(Card cardBeforeEdit, Card editedCard)
        {
            ActivityLogType logType = await _activityLogRepository.GetActivityLogTypeByName("Edit");
            Models.ActivityLog activityLog = new Models.ActivityLog
            {
                Id = Guid.NewGuid(),
                ChangedCardId = cardBeforeEdit.Id,
                ChangedCardTitle = editedCard.Title,
                ChangedFieldName = nameof(cardBeforeEdit.Priority),
                ValueBefore = await _cardRepository.GetPriorityNameById(cardBeforeEdit.PriorityId),
                ValueAfter = await _cardRepository.GetPriorityNameById(editedCard.PriorityId),
                CreationDate = DateTime.Now.ToUniversalTime(),
                ActivityLogTypeId = logType.Id,
                BoardId = cardBeforeEdit.TaskList.BoardId
            };
            await _activityLogRepository.AddActivityLog(activityLog);
        }

        private async Task GenerateMoveLog(Card cardBeforeEdit, Card editedCard)
        {
            ActivityLogType logType = await _activityLogRepository.GetActivityLogTypeByName("Move");
            Models.ActivityLog activityLog = new Models.ActivityLog
            {
                Id = Guid.NewGuid(),
                ChangedCardId = cardBeforeEdit.Id,
                ChangedCardTitle = editedCard.Title,
                ChangedFieldName = nameof(cardBeforeEdit.TaskList),
                ValueBefore = await _taskListRepository.GetTaskListNameById(cardBeforeEdit.TaskListId),
                ValueAfter = await _taskListRepository.GetTaskListNameById(editedCard.TaskListId),
                CreationDate = DateTime.Now.ToUniversalTime(),
                ActivityLogTypeId = logType.Id,
                BoardId = cardBeforeEdit.TaskList.BoardId
            };
            await _activityLogRepository.AddActivityLog(activityLog);
        }

        private async Task GenerateEditDescriptionLog(Card cardBeforeEdit, Card editedCard)
        {
            ActivityLogType logType = await _activityLogRepository.GetActivityLogTypeByName("Edit");
            Models.ActivityLog activityLog = new Models.ActivityLog
            {
                Id = Guid.NewGuid(),
                ChangedCardId = cardBeforeEdit.Id,
                ChangedCardTitle = editedCard.Title,
                ChangedFieldName = nameof(cardBeforeEdit.Description),
                CreationDate = DateTime.Now.ToUniversalTime(),
                ActivityLogTypeId = logType.Id,
                BoardId = cardBeforeEdit.TaskList.BoardId
            };
            await _activityLogRepository.AddActivityLog(activityLog);
        }

        private async Task GenerateCardEditDueDateLog(Card cardBeforeEdit, Card editedCard)
        {
            ActivityLogType logType = await _activityLogRepository.GetActivityLogTypeByName("Edit");
            Models.ActivityLog activityLog = new Models.ActivityLog
            {
                Id = Guid.NewGuid(),
                ChangedCardId = cardBeforeEdit.Id,
                ChangedCardTitle = editedCard.Title,
                ChangedFieldName = nameof(cardBeforeEdit.DueDate),
                ValueBefore = cardBeforeEdit.DueDate.ToString(),
                ValueAfter = editedCard.DueDate.ToString(),
                CreationDate = DateTime.Now.ToUniversalTime(),
                ActivityLogTypeId = logType.Id,
                BoardId = cardBeforeEdit.TaskList.BoardId
            };
            await _activityLogRepository.AddActivityLog(activityLog);
        }

        private async Task GenerateRenameLog(Card cardBeforeEdit, Card editedCard)
        {
            ActivityLogType logType = await _activityLogRepository.GetActivityLogTypeByName("Rename");
            Models.ActivityLog activityLog = new Models.ActivityLog
            {
                Id = Guid.NewGuid(),
                ChangedCardId = cardBeforeEdit.Id,
                ChangedCardTitle = editedCard.Title,
                ChangedFieldName = nameof(cardBeforeEdit.Title),
                ValueBefore = cardBeforeEdit.Title,
                ValueAfter = editedCard.Title,
                CreationDate = DateTime.Now.ToUniversalTime(),
                ActivityLogTypeId = logType.Id,
                BoardId = cardBeforeEdit.TaskList.BoardId
            };
            await _activityLogRepository.AddActivityLog(activityLog);
        }
    }
}
