using First_App.Server.Models.DTOs;
using First_App.Server.Models.RequestModels.Board;
using FluentAssertions;
using System.Net;
using System.Net.Http.Json;

namespace First_App.Test.IntegrationTests
{
    public class BoardControllerTests
    {
        [Fact]
        public async Task AddBoard_ReturnsOk_WhenCommandIsValid()
        {
            // Arrange
            var app = new FirstAppFactory();
            CreateBoardCommand command = new CreateBoardCommand { Name = "TestTaskBoard" };
            var client = app.CreateClient();
            // Act
            var response = await client.PostAsJsonAsync("/api/Board/add", command);
            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Fact]
        public async Task AddBoard_ReturnsBadRequest_WhenCommandIsNotValid()
        {
            // Arrange
            var app = new FirstAppFactory();
            CreateBoardCommand command = new CreateBoardCommand { Name = "" };
            var client = app.CreateClient();
            // Act
            var response = await client.PostAsJsonAsync("/api/Board/add", command);
            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task EditBoard_ReturnsOk_WhenCommandIsValid()
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

            EditBoardCommand command = new EditBoardCommand { Name = "TestTaskBoard edited" };

            // Act
            var response = await client.PatchAsJsonAsync("/api/Board/edit/" + testBoard.Id, command);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Fact]
        public async Task EditBoard_ReturnsBadRequest_WhenCommandIsNotValid()
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

            EditBoardCommand command = new EditBoardCommand { Name = "" };

            // Act
            var response = await client.PatchAsJsonAsync("/api/Board/edit/" + testBoard.Id, command);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task EditBoard_ReturnsBadRequest_WhenIdIsNotFound()
        {
            // Arrange
            var app = new FirstAppFactory();
            var client = app.CreateClient();

            EditBoardCommand command = new EditBoardCommand { Name = "" };

            // Act
            var response = await client.PatchAsJsonAsync("/api/Board/edit/" + Guid.NewGuid(), command);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task DeleteBoard_ReturnsNoContent_WhenIdIsValid()
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

            // Act
            var response = await client.DeleteAsync("/api/Board/delete/" + testBoard.Id);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.NoContent);
        }

        [Fact]
        public async Task DeleteBoard_ReturnsNotFound_WhenIdIsNotFound()
        {
            // Arrange
            var app = new FirstAppFactory();
            var client = app.CreateClient();

            // Act
            var response = await client.DeleteAsync("/api/Board/delete/" + Guid.NewGuid());

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

    }
}
