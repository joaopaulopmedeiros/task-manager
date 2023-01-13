using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Threading.Tasks;
using TaskManager.Api.Controllers;
using TaskManager.Api.Requests;
using TaskManager.Api.Responses;
using TaskManager.Api.Services;
using Xunit;

namespace TaskManager.Api.Unit.Tests
{
    public class TasksControllerTests
    {
        [Fact]
        public async Task MustReturn200StatusCode()
        {
            //arrange
            var request = new TaskSearchRequest
            {
                Page=1,
                Size=10,
            };

            var mockService = new Mock<ITaskSearchService>();

            var tasks = new TaskSearchResponse
            {
              new Infrastructure.Models.Task
              {
                Id = System.Guid.NewGuid(),
                Title = "Lorem Ipsum",
                Author = "joaopaulopmedeiros@gmail.com",
                CreatedAt = System.DateTime.Now,
                Status = Infrastructure.Models.TaskStatusEnum.Created,
              }
            };

            mockService.Setup(s => s.SearchByAsync(request)).ReturnsAsync(tasks);

            var sut = new TasksController();

            //act 
            var response = await sut.SearchByAsync(request, mockService.Object);

            //assert
            response.Should().BeOfType<OkObjectResult>();
        }
    }
}