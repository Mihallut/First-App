using AutoMapper;
using First_App.Server.Helpers.Interfases;
using First_App.Server.Models;
using First_App.Server.Models.DTOs;
using First_App.Server.Models.RequestModels.Card;
using First_App.Server.Repositories.Interfaces;
using MediatR;

namespace First_App.Server.Handlers.Cards
{
    public class EditCardCommandHandler : IRequestHandler<EditCardCommand, CardDto>
    {
        ICardRepository _cardRepository;
        IMapper _mapper;
        IActivityLogGenerator _activityLogGenerator;

        public EditCardCommandHandler(ICardRepository cardRepository, IMapper mapper, IActivityLogGenerator activityLogGenerator)
        {
            _cardRepository = cardRepository;
            _mapper = mapper;
            _activityLogGenerator = activityLogGenerator;
        }

        public async Task<CardDto> Handle(EditCardCommand request, CancellationToken cancellationToken)
        {
            Card cardToEdit = new Card
            {
                Id = request.Id,
                Title = request.Title,
                PriorityId = request.PriorityId,
                TaskListId = request.TaskListId,
                Description = request.Description,
                DueDate = request.DueDate
            };
            var cardBeforeEdit = await _cardRepository.GetCardById(cardToEdit.Id);
            await _activityLogGenerator.GenerateEditLog(cardBeforeEdit, cardToEdit);

            var editedCard = await _cardRepository.EditCard(cardToEdit);

            var cardDto = _mapper.Map<CardDto>(editedCard);
            return cardDto;
        }
    }
}
