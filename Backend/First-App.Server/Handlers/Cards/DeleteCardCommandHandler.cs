using First_App.Server.Helpers.Interfases;
using First_App.Server.Models.RequestModels.Card;
using First_App.Server.Repositories.Interfaces;
using MediatR;

namespace First_App.Server.Handlers.Cards
{
    public class DeleteCardCommandHandler : IRequestHandler<DeleteCardCommand>
    {
        private readonly ICardRepository _cardRepository;
        private readonly IActivityLogGenerator _activityLogGenerator;

        public DeleteCardCommandHandler(ICardRepository cardRepository, IActivityLogGenerator activityLogGenerator)
        {
            _cardRepository = cardRepository;
            _activityLogGenerator = activityLogGenerator;
        }

        public async Task Handle(DeleteCardCommand request, CancellationToken cancellationToken)
        {
            var card = await _cardRepository.GetCardById(request.Id);
            await _activityLogGenerator.GenerateDeleteLog(card);
            await _cardRepository.DeleteCard(card.Id);
        }
    }
}
