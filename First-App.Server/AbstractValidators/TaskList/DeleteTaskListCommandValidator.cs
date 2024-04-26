using First_App.Server.Models.RequestModels.TaskList;
using First_App.Server.Repositories.Interfaces;
using FluentValidation;

namespace First_App.Server.AbstractValidators.TaskList
{
    public class DeleteTaskListCommandValidator : AbstractValidator<DeleteTaskListCommand>
    {
        private readonly ITaskListRepository _taskListRepository;

        public DeleteTaskListCommandValidator(ITaskListRepository taskListRepository)
        {
            _taskListRepository = taskListRepository;

            RuleFor(x => x.Id)
                .NotEmpty()
                .Must(ExistInDb)
                .WithMessage("Task list with provided guid does not exist in database.");
        }

        private bool ExistInDb(Guid guid)
        {
            var taskList = _taskListRepository.GetTaskListById(guid);
            return taskList.Result != null;
        }
    }
}
