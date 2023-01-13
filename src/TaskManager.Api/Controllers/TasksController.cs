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
        /// Consulta por tarefas
        /// </summary>
        /// <param name="request"></param>
        /// <param name="service"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> SearchByAsync
        (
            [FromQuery] TaskSearchRequest request,
            [FromServices] TaskSearchService service
        )
        {
            var response = await service.SearchByAsync(request);
            
            if (response == null || !response.Any()) return NoContent();

            return Ok(response);
        }
    }
}