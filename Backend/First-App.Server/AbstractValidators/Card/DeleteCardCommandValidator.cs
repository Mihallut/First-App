using First_App.Server.Models.RequestModels.Card;
using First_App.Server.Repositories.Interfaces;
using FluentValidation;

namespace First_App.Server.AbstractValidators.Card
{
    public class DeleteCardCommandValidator : AbstractValidator<DeleteCardCommand>
    {
        private readonly ICardRepository _cardRepository;

        public DeleteCardCommandValidator(ICardRepository cardRepository)
        {
            _cardRepository = cardRepository;

            RuleFor(x => x.Id)
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
