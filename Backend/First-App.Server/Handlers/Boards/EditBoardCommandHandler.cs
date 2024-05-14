using AutoMapper;
using First_App.Server.Models.DTOs;
using First_App.Server.Models.RequestModels.Board;
using First_App.Server.Repositories.Interfaces;
using MediatR;

namespace First_App.Server.Handlers.Boards
{
    public class EditBoardCommandHandler : IRequestHandler<EditBoardCommand, BoardDto>
    {
        private readonly IBoardRepository _boardRepository;
        private readonly IMapper _mapper;

        public EditBoardCommandHandler(IBoardRepository boardRepository, IMapper mapper)
        {
            _mapper = mapper;
            _boardRepository = boardRepository;
        }

        public async Task<BoardDto> Handle(EditBoardCommand request, CancellationToken cancellationToken)
        {
            var result = await _boardRepository.EditBoard(request.Id, request.Name);
            var resultDto = _mapper.Map<BoardDto>(result);
            return resultDto;
        }
    }
}