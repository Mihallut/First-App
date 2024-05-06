using First_App.Server.Models.RequestModels.TaskList;
using First_App.Server.Repositories.Interfaces;
using FluentValidation;

namespace First_App.Server.AbstractValidators.TaskList
{
    public class CreateTaskListCommandValidator : AbstractValidator<CreateTaskListCommand>
    {
        private readonly ITaskListRepository _taskListRepository;

        public CreateTaskListCommandValidator(ITaskListRepository taskListRepository)
        {
            _taskListRepository = taskListRepository;

            RuleFor(x => x.Name)
                .NotEmpty()
                .Length(1, 300)
                .Matches(@"^[A-Za-z0-9\s-]*$")
                .WithMessage("Project name must be at least 1 character long and no longer than 300 characters. You may use Latin letters only. Digits, special symbols, hyphens and spaces are allowed")
                .Must(BeUniqueName)
                .WithMessage("This task list already created.");
        }

        private bool BeUniqueName(string name)
        {
            var taskList = _taskListRepository.GetTaskListByName(name);
            return taskList.Result == null;
        }
    }
}
