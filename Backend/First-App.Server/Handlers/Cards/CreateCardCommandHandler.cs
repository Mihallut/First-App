using AutoMapper;
using First_App.Server.Helpers.Interfases;
using First_App.Server.Models;
using First_App.Server.Models.DTOs;
using First_App.Server.Models.RequestModels.Card;
using First_App.Server.Repositories.Interfaces;
using MediatR;

namespace First_App.Server.Handlers.Cards
{
    public class CreateCardCommandHandler : IRequestHandler<CreateCardCommand, CardDto>
    {
        ICardRepository _cardRepository;
        IMapper _mapper;
        IActivityLogGenerator _activityLogGenerator;

        public CreateCardCommandHandler(ICardRepository cardRepository, IMapper mapper, IActivityLogGenerator activityLogGenerator)
        {
            _cardRepository = cardRepository;
            _mapper = mapper;
            _activityLogGenerator = activityLogGenerator;
        }
        public async Task<CardDto> Handle(CreateCardCommand request, CancellationToken cancellationToken)
        {
            Card card = new Card
            {
                Id = Guid.NewGuid(),
                Title = request.Title,
                Description = request.Description,
                DueDate = request.DueDate,
                TaskListId = request.TaskListId,
                PriorityId = request.PriorityId
            };

            var addedCard = await _cardRepository.AddCard(card);
            if (addedCard != null)
            {
                await _activityLogGenerator.GenerateCreateLog(card);
            }
            var cardDto = _mapper.Map<CardDto>(addedCard);

            return cardDto;
        }
    }
}
