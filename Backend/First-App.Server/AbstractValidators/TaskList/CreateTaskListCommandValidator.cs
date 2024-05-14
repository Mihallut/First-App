using First_App.Server.Models.RequestModels.TaskList;
using First_App.Server.Repositories.Interfaces;
using FluentValidation;

namespace First_App.Server.AbstractValidators.TaskList
{
    public class CreateTaskListCommandValidator : AbstractValidator<CreateTaskListCommand>
    {
        private readonly ITaskListRepository _taskListRepository;
        private readonly IBoardRepository _boardRepository;

        public CreateTaskListCommandValidator(ITaskListRepository taskListRepository, IBoardRepository boardRepository)
        {
            _taskListRepository = taskListRepository;
            _boardRepository = boardRepository;

            RuleFor(x => x.Name)
                .NotEmpty()
                .Length(1, 300)
                .Matches(@"^[A-Za-z0-9\s-]*$")
                .WithMessage("Project name must be at least 1 character long and no longer than 300 characters. You may use Latin letters only. Digits, special symbols, hyphens and spaces are allowed")
                .Must((model, Name) => BeUniqueName(model.BoardId, Name))
                .WithMessage("This task list already created.");

            RuleFor(x => x.BoardId)
             .NotEmpty()
             .Must(ExistInDb)
             .WithMessage("Board with provided guid does not exist in database.");
        }

        private bool BeUniqueName(Guid boardId, string name)
        {
            var taskList = _taskListRepository.GetTaskListByName(boardId, name);
            return taskList.Result == null;
        }

        private bool ExistInDb(Guid guid)
        {
            var board = _boardRepository.GetBoardById(guid);
            return board.Result != null;
        }
    }
}
