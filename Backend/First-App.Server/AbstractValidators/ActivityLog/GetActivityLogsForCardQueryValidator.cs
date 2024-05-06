using First_App.Server.Models.RequestModels.ActivityLog;
using First_App.Server.Repositories.Interfaces;
using FluentValidation;

namespace First_App.Server.AbstractValidators.ActivityLog
{
    public class GetActivityLogsForCardQueryValidator : AbstractValidator<GetActivityLogsForCardQuery>
    {
        ICardRepository _cardRepository;

        public GetActivityLogsForCardQueryValidator(ICardRepository cardRepository)
        {
            _cardRepository = cardRepository;

            RuleFor(x => x.PageNumber)
             .GreaterThan(0)
             .WithMessage("Page number should be greater than 0.");

            RuleFor(x => x.PageSize)
              .GreaterThan(0)
              .WithMessage("Page size should be greater than 0.");

            RuleFor(x => x.SortField)
                .NotNull()
                .WithMessage("The provided sort field value does not match any known options.");

            RuleFor(x => x.SortOrder)
                .NotNull()
                .WithMessage("The provided sort order is invalid. Only 'Ascending' or 'Descending' values are allowed.");

            RuleFor(x => x.CardId)
                .NotEmpty()
                .Must(ExistInDb)
                .WithMessage("Card with provided guid does not exist in database.");
        }

        private bool ExistInDb(Guid guid)
        {
            var taskList = _cardRepository.GetCardById(guid);
            return taskList.Result != null;
        }
    }
}
