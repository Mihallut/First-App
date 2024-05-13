using AutoMapper;
using First_App.Server.Handlers.TaskLists;
using First_App.Server.Models;
using First_App.Server.Models.DTOs;
using First_App.Server.Models.RequestModels.TaskList;
using First_App.Server.Repositories.Interfaces;
using Moq;

namespace First_App.Test.UnitTests
{
    public class CreateTaskListCommandHandlerTests
    {
        private readonly Mock<ITaskListRepository> _mockTaskListRepository;
        private readonly Mock<IMapper> _mockMapper;
        private readonly CreateTaskListCommandHandler _handler;

        public CreateTaskListCommandHandlerTests()
        {
            _mockTaskListRepository = new Mock<ITaskListRepository>();
            _mockMapper = new Mock<IMapper>();
            _handler = new CreateTaskListCommandHandler(_mockTaskListRepository.Object, _mockMapper.Object);
        }

        [Fact]
        public async Task Handle_ValidCommand_AddsTaskListAndReturnsTaskListDto()
        {
            // Arrange
            var command = new CreateTaskListCommand { Name = "Test TaskList", BoardId = Guid.NewGuid() };
            var taskList = new TaskList { Id = Guid.NewGuid(), Name = command.Name, CreationDate = DateTime.Now.ToUniversalTime(), BoardId = command.BoardId };
            var taskListDto = new TaskListDto { Id = taskList.Id, Name = taskList.Name };

            _mockTaskListRepository.Setup(repo => repo.AddTaskList(It.IsAny<TaskList>())).ReturnsAsync(taskList);
            _mockMapper.Setup(mapper => mapper.Map<TaskListDto>(It.IsAny<TaskList>())).Returns(taskListDto);

            // Act
            var result = await _handler.Handle(command, default);

            // Assert
            Assert.Equal(taskListDto, result);
            _mockTaskListRepository.Verify(repo => repo.AddTaskList(It.IsAny<TaskList>()), Times.Once());
            _mockMapper.Verify(mapper => mapper.Map<TaskListDto>(It.IsAny<TaskList>()), Times.Once());
        }
    }
}
