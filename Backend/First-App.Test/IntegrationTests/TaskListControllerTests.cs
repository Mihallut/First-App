using First_App.Server.Models.DTOs;
using First_App.Server.Models.RequestModels.Board;
using First_App.Server.Models.RequestModels.TaskList;
using FluentAssertions;
using System.Net;
using System.Net.Http.Json;

namespace First_App.Test.IntegrationTests
{
    public class TaskListControllerTests
    {
        [Fact]
        public async Task AddTaskList_ReturnsOk_WhenCommandIsValid()
        {
            // Arrange
            var app = new FirstAppFactory();
            CreateBoardCommand createCommand = new CreateBoardCommand { Name = "TestTaskBoard" };
            var client = app.CreateClient();
            var createResponse = await client.PostAsJsonAsync("/api/Board/add", createCommand);

            // Ensure the response from create is successful
            createResponse.EnsureSuccessStatusCode();

            // Deserialize the response content to BoardDto
            BoardDto testBoard = await createResponse.Content.ReadFromJsonAsync<BoardDto>();

            CreateTaskListCommand command = new CreateTaskListCommand { BoardId = testBoard.Id, Name = "Test Task List" };
            // Act
            var response = await client.PostAsJsonAsync("/api/TaskList/add", command);
            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Fact]
        public async Task AddTaskList_ReturnsBadRequest_WhenCommandIsNotValid()
        {
            // Arrange
            var app = new FirstAppFactory();
            CreateBoardCommand createCommand = new CreateBoardCommand { Name = "TestTaskBoard" };
            var client = app.CreateClient();
            var createResponse = await client.PostAsJsonAsync("/api/Board/add", createCommand);

            // Ensure the response from create is successful
            createResponse.EnsureSuccessStatusCode();

            // Deserialize the response content to BoardDto
            BoardDto testBoard = await createResponse.Content.ReadFromJsonAsync<BoardDto>();

            CreateTaskListCommand command = new CreateTaskListCommand { BoardId = testBoard.Id, Name = "" };
            // Act
            var response = await client.PostAsJsonAsync("/api/TaskList/add", command);
            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task AddTaskList_ReturnsBadRequest_WhenBoardIdIsNotValid()
        {
            // Arrange
            var app = new FirstAppFactory();
            var client = app.CreateClient();
            CreateTaskListCommand command = new CreateTaskListCommand { BoardId = Guid.NewGuid(), Name = "Test Task List" };
            // Act
            var response = await client.PostAsJsonAsync("/api/TaskList/add", command);
            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task EditTaskList_ReturnsOk_WhenCommandIsValid()
        {
            // Arrange
            var app = new FirstAppFactory();
            CreateBoardCommand createBoardCommand = new CreateBoardCommand { Name = "TestTaskBoard" };
            var client = app.CreateClient();
            var createBoardResponse = await client.PostAsJsonAsync("/api/Board/add", createBoardCommand);

            // Ensure the response from create is successful
            createBoardResponse.EnsureSuccessStatusCode();

            // Deserialize the response content to BoardDto
            BoardDto testBoard = await createBoardResponse.Content.ReadFromJsonAsync<BoardDto>();

            CreateTaskListCommand createTaskListCommand = new CreateTaskListCommand { BoardId = testBoard.Id, Name = "Test Task List" };
            var createTaskListResponse = await client.PostAsJsonAsync("/api/TaskList/add", createTaskListCommand);
            // Ensure the response from create is successful
            createTaskListResponse.EnsureSuccessStatusCode();

            TaskListDto testTaskList = await createTaskListResponse.Content.ReadFromJsonAsync<TaskListDto>();

            EditTaskListCommand command = new EditTaskListCommand { NewName = "Test Task List edited", BoardId = testBoard.Id };
            // Act
            var response = await client.PatchAsJsonAsync("/api/TaskList/edit/" + testTaskList.Id, command);
            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Fact]
        public async Task EditTaskList_ReturnsBadRequest_WhenCommandIsNotValid()
        {
            // Arrange
            var app = new FirstAppFactory();
            CreateBoardCommand createBoardCommand = new CreateBoardCommand { Name = "TestTaskBoard" };
            var client = app.CreateClient();
            var createBoardResponse = await client.PostAsJsonAsync("/api/Board/add", createBoardCommand);

            // Ensure the response from create is successful
            createBoardResponse.EnsureSuccessStatusCode();

            // Deserialize the response content to BoardDto
            BoardDto testBoard = await createBoardResponse.Content.ReadFromJsonAsync<BoardDto>();

            CreateTaskListCommand createTaskListCommand = new CreateTaskListCommand { BoardId = testBoard.Id, Name = "Test Task List" };
            var createTaskListResponse = await client.PostAsJsonAsync("/api/TaskList/add", createTaskListCommand);
            // Ensure the response from create is successful
            createTaskListResponse.EnsureSuccessStatusCode();

            TaskListDto testTaskList = await createTaskListResponse.Content.ReadFromJsonAsync<TaskListDto>();

            EditTaskListCommand command = new EditTaskListCommand { NewName = "", BoardId = testBoard.Id };
            // Act
            var response = await client.PatchAsJsonAsync("/api/TaskList/edit/" + testTaskList.Id, command);
            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }
    }
}
