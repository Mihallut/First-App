using AutoMapper;
using First_App.Server.Handlers.Boards;
using First_App.Server.Models.DTOs;
using First_App.Server.Models.RequestModels.Board;
using First_App.Server.Repositories.Interfaces;
using Moq;

namespace First_App.Test.UnitTests
{
    public class CreateBoardCommandHandlerTests
    {
        private readonly Mock<IBoardRepository> _mockBoardRepository;
        private readonly Mock<IMapper> _mockMapper;
        private readonly CreateBoardCommandHandler _handler;

        public CreateBoardCommandHandlerTests()
        {
            _mockBoardRepository = new Mock<IBoardRepository>();
            _mockMapper = new Mock<IMapper>();
            _handler = new CreateBoardCommandHandler(_mockBoardRepository.Object, _mockMapper.Object);
        }

        [Fact]
        public async Task Handle_ValidCommand_AddsBoardAndReturnsBoardDto()
        {
            // Arrange
            var command = new CreateBoardCommand { Name = "Test Board" };
            var board = new Server.Models.Board { Id = Guid.NewGuid(), Name = command.Name, CreationDate = DateTime.Now.ToUniversalTime() };
            var boardDto = new BoardDto { Id = board.Id, Name = board.Name };

            _mockBoardRepository.Setup(repo => repo.AddBoard(It.IsAny<Server.Models.Board>())).ReturnsAsync(board);
            _mockMapper.Setup(mapper => mapper.Map<BoardDto>(It.IsAny<Server.Models.Board>())).Returns(boardDto);

            // Act
            var result = await _handler.Handle(command, default);

            // Assert
            Assert.Equal(boardDto, result);
            _mockBoardRepository.Verify(repo => repo.AddBoard(It.IsAny<Server.Models.Board>()), Times.Once());
            _mockMapper.Verify(mapper => mapper.Map<BoardDto>(It.IsAny<Server.Models.Board>()), Times.Once());
        }
    }
}
