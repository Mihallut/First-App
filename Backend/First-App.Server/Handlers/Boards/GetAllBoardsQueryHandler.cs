using AutoMapper;
using First_App.Server.Models.DTOs;
using First_App.Server.Models.RequestModels.Board;
using First_App.Server.Repositories.Interfaces;
using MediatR;

namespace First_App.Server.Handlers.Boards
{
    public class GetBoardsQueryHandler : IRequestHandler<GetAllBoardsQuery, List<BoardDto>>
    {
        private readonly IBoardRepository _boardRepository;
        private readonly IMapper _mapper;

        public GetBoardsQueryHandler(IBoardRepository boardRepository, IMapper mapper)
        {
            _mapper = mapper;
            _boardRepository = boardRepository;
        }
        public async Task<List<BoardDto>> Handle(GetAllBoardsQuery request, CancellationToken cancellationToken)
        {
            var result = await _boardRepository.GetAllBoards();
            if (result.Count == 0)
            {
                return null;
            }
            var resultDto = _mapper.Map<List<BoardDto>>(result);
            return resultDto;
        }
    }
}
