using AutoMapper;
using First_App.Server.Handlers.Boards;
using First_App.Server.Models.DTOs;
using First_App.Server.Models.RequestModels.Board;
using First_App.Server.Repositories.Interfaces;
using Moq;

namespace First_App.Test.UnitTests
{
    public class EditBoardCommandHandlerTests
    {
        private readonly Mock<IBoardRepository> _mockBoardRepository;
        private readonly Mock<IMapper> _mockMapper;
        private readonly EditBoardCommandHandler _handler;

        public EditBoardCommandHandlerTests()
        {
            _mockBoardRepository = new Mock<IBoardRepository>();
            _mockMapper = new Mock<IMapper>();
            _handler = new EditBoardCommandHandler(_mockBoardRepository.Object, _mockMapper.Object);
        }

        [Fact]
        public async Task Handle_ValidCommand_EditsBoardAndReturnsBoardDto()
        {
            // Arrange
            var command = new EditBoardCommand { Id = Guid.NewGuid(), Name = "Test Board" };
            var board = new Server.Models.Board { Id = command.Id, Name = command.Name, CreationDate = DateTime.Now.ToUniversalTime() };
            var boardDto = new BoardDto { Id = board.Id, Name = board.Name };

            _mockBoardRepository.Setup(repo => repo.EditBoard(It.IsAny<Guid>(), It.IsAny<string>())).ReturnsAsync(board);
            _mockMapper.Setup(mapper => mapper.Map<BoardDto>(It.IsAny<Server.Models.Board>())).Returns(boardDto);

            // Act
            var result = await _handler.Handle(command, default);

            // Assert
            Assert.Equal(boardDto, result);
            _mockBoardRepository.Verify(repo => repo.EditBoard(It.IsAny<Guid>(), It.IsAny<string>()), Times.Once());
            _mockMapper.Verify(mapper => mapper.Map<BoardDto>(It.IsAny<Server.Models.Board>()), Times.Once());
        }
    }
}
