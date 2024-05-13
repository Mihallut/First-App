using First_App.Server.Handlers.Board;
using First_App.Server.Models;
using First_App.Server.Models.RequestModels.Board;
using First_App.Server.Repositories.Interfaces;
using Moq;

namespace First_App.Test.UnitTests
{
    public class DeleteBoardCommandHandlerTests
    {
        private readonly Mock<ITaskListRepository> _mockTaskListRepository;
        private readonly Mock<ICardRepository> _mockCardRepository;
        private readonly Mock<IBoardRepository> _mockBoardRepository;
        private readonly DeleteBoardCommandHandler _handler;

        public DeleteBoardCommandHandlerTests()
        {
            _mockTaskListRepository = new Mock<ITaskListRepository>();
            _mockCardRepository = new Mock<ICardRepository>();
            _mockBoardRepository = new Mock<IBoardRepository>();
            _handler = new DeleteBoardCommandHandler(_mockTaskListRepository.Object, _mockCardRepository.Object, _mockBoardRepository.Object);
        }

        [Fact]
        public async Task Handle_ValidCommand_DeletesBoardAndAllRelatedEntities()
        {
            // Arrange
            var command = new DeleteBoardCommand { Id = Guid.NewGuid() };
            var taskLists = new List<TaskList>
        {
            new TaskList { Id = Guid.NewGuid(), Cards = new List<Card> { new Card { Id = Guid.NewGuid() } } },
            new TaskList { Id = Guid.NewGuid(), Cards = new List<Card> { new Card { Id = Guid.NewGuid() } } }
        };

            _mockTaskListRepository.Setup(repo => repo.GetAllTaskLists(It.IsAny<Guid>())).ReturnsAsync(taskLists);
            _mockCardRepository.Setup(repo => repo.DeleteCard(It.IsAny<Guid>())).Returns(Task.CompletedTask);
            _mockTaskListRepository.Setup(repo => repo.DeleteTaskList(It.IsAny<Guid>())).Returns(Task.CompletedTask);
            _mockBoardRepository.Setup(repo => repo.DeleteBoard(It.IsAny<Guid>())).Returns(Task.CompletedTask);

            // Act
            await _handler.Handle(command, default);

            // Assert
            foreach (var taskList in taskLists)
            {
                foreach (var card in taskList.Cards)
                {
                    _mockCardRepository.Verify(repo => repo.DeleteCard(card.Id), Times.Once());
                }
                _mockTaskListRepository.Verify(repo => repo.DeleteTaskList(taskList.Id), Times.Once());
            }
            _mockBoardRepository.Verify(repo => repo.DeleteBoard(command.Id), Times.Once());
        }
    }
}
