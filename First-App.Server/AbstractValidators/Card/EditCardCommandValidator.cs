using First_App.Server.Models.RequestModels.Card;
using First_App.Server.Repositories.Interfaces;
using FluentValidation;

namespace First_App.Server.AbstractValidators.Card
{
    public class EditCardCommandValidator : AbstractValidator<EditCardCommand>
    {
        ITaskListRepository _taskListRepository;
        ICardRepository _cardRepository;
        public EditCardCommandValidator(ICardRepository cardRepository, ITaskListRepository taskListRepository)
        {
            _taskListRepository = taskListRepository;
            _cardRepository = cardRepository;

            RuleFor(x => x.Title)
                .NotEmpty()
                .Length(1, 300)
                .Matches(@"^[A-Za-z0-9\s-]*$")
                .WithMessage("Project name must be at least 1 character long and no longer than 300 characters. You may use Latin letters only. Digits, special symbols, hyphens and spaces are allowed");

            RuleFor(x => x.DueDate)
                .Cascade(CascadeMode.Stop)
                .NotEmpty()
                .Must(BeNotYesterday)
                .WithMessage("Yesterday's date or earlier. Please choose another date no earlier than today.");

            RuleFor(x => x.PriorityId)
                .NotEmpty()
                .Must(ContainsInDB)
                .WithMessage("Provided proirity id does not contains in database.");

            RuleFor(x => x.TaskListId)
                .NotEmpty()
                .Must(ContainsInDB)
                .WithMessage("Provided task list id does not contains in database.");
        }

        private bool ContainsInDB(Guid guid)
        {
            var result = _taskListRepository.GetTaskListById(guid);
            return result.Result != null;
        }

        private bool ContainsInDB(uint arg)
        {
            var result = _cardRepository.GetPriorityById(arg);
            return result.Result != null;
        }

        private bool BeNotYesterday(DateOnly dueDate)
        {
            return dueDate > DateOnly.FromDateTime(DateTime.Today.AddDays(-1));
        }
    }
}

