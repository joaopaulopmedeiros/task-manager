using TaskManager.Api.Requests;
using TaskManager.Api.Responses;

namespace TaskManager.Api.Services
{
    public class TaskSearchService
    {
        public async Task<TaskSearchResponse> SearchByAsync(TaskSearchRequest request)
        {
            await Task.Delay(1000);
            return new TaskSearchResponse();
        }
    }
}
