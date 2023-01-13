using TaskManager.Api.Requests;
using TaskManager.Api.Responses;

namespace TaskManager.Api.Services
{
    public interface ITaskSearchService
    {
        public Task<TaskSearchResponse?> SearchByAsync(TaskSearchRequest request);
    }
}
