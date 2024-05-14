using AutoMapper;
using First_App.Server.Handlers.TaskLists;
using First_App.Server.Models;
using First_App.Server.Models.DTOs;
using First_App.Server.Models.RequestModels.TaskList;
using First_App.Server.Repositories.Interfaces;
using Moq;

namespace First_App.Test.UnitTests
{
    public class EditTaskListCommandHandlerTests
    {
        private readonly Mock<ITaskListRepository> _mockTaskListRepository;
        private readonly Mock<IMapper> _mockMapper;
        private readonly EditTaskListCommandHandler _handler;

        public EditTaskListCommandHandlerTests()
        {
            _mockTaskListRepository = new Mock<ITaskListRepository>();
            _mockMapper = new Mock<IMapper>();
            _handler = new EditTaskListCommandHandler(_mockTaskListRepository.Object, _mockMapper.Object);
        }

        [Fact]
        public async Task Handle_ValidCommand_EditsTaskListAndReturnsTaskListDto()
        {
            // Arrange
            var command = new EditTaskListCommand { Id = Guid.NewGuid(), NewName = "Test TaskList" };
            var taskList = new TaskList { Id = command.Id, Name = command.NewName, CreationDate = DateTime.Now.ToUniversalTime() };
            var taskListDto = new TaskListDto { Id = taskList.Id, Name = taskList.Name };

            _mockTaskListRepository.Setup(repo => repo.EditTaskList(It.IsAny<Guid>(), It.IsAny<string>())).ReturnsAsync(taskList);
            _mockMapper.Setup(mapper => mapper.Map<TaskListDto>(It.IsAny<TaskList>())).Returns(taskListDto);

            // Act
            var result = await _handler.Handle(command, default);

            // Assert
            Assert.Equal(taskListDto, result);
            _mockTaskListRepository.Verify(repo => repo.EditTaskList(It.IsAny<Guid>(), It.IsAny<string>()), Times.Once());
            _mockMapper.Verify(mapper => mapper.Map<TaskListDto>(It.IsAny<TaskList>()), Times.Once());
        }
    }
}
