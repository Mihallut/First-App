using AutoMapper;
using First_App.Server.Models.DTOs;
using First_App.Server.Models.RequestModels.Board;
using First_App.Server.Repositories.Interfaces;
using MediatR;

namespace First_App.Server.Handlers.Boards
{
    public class CreateBoardCommandHandler : IRequestHandler<CreateBoardCommand, BoardDto>
    {
        private readonly IBoardRepository _boardRepository;
        private readonly IMapper _mapper;

        public CreateBoardCommandHandler(IBoardRepository boardRepository, IMapper mapper)
        {
            _mapper = mapper;
            _boardRepository = boardRepository;
        }

        public async Task<BoardDto> Handle(CreateBoardCommand request, CancellationToken cancellationToken)
        {
            var board = new Models.Board
            {
                Id = Guid.NewGuid(),
                Name = request.Name,
                CreationDate = DateTime.Now.ToUniversalTime(),
            };
            var addedBoard = await _boardRepository.AddBoard(board);

            var boardDto = _mapper.Map<BoardDto>(addedBoard);

            return boardDto;
        }
    }
}
