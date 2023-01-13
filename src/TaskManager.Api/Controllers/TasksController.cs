using Microsoft.AspNetCore.Mvc;
using TaskManager.Api.Requests;
using TaskManager.Api.Services;

namespace TaskManager.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TasksController : ControllerBase
    {
        /// <summary>
        /// Simple search for tasks
        /// </summary>
        /// <response code="200">Returns 200 and list of tasks</response>
        /// <response code="204">Returns 204 and an empty list of tasks, which means that there are not results for search</response>
        /// <returns>List of tasks</returns>
        /// <param name="request"></param>
        /// <param name="service"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> SearchByAsync
        (
            [FromQuery] TaskSearchRequest request,
            [FromServices] ITaskSearchService service
        )
        {
            var response = await service.SearchByAsync(request);
            
            if (response == null || !response.Any()) return NoContent();

            return Ok(response);
        }
    }
}