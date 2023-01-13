using Microsoft.EntityFrameworkCore;
using TaskManager.Api.Requests;
using TaskManager.Api.Responses;
using TaskManager.Infrastructure;

namespace TaskManager.Api.Services
{
    public class TaskSearchService
    {
        private readonly TaskDbContext _context;

        public TaskSearchService(TaskDbContext context) => _context = context;

        public async Task<TaskSearchResponse> SearchByAsync(TaskSearchRequest request)
        {
            var tasks = await _context
                           .Tasks
                           .Where(t => EF.Functions.Like(t.Title, $"%{request.Title}%"))
                           .Skip((request.Page - 1) * request.Size)
                           .Take(request.Size)
                           .ToListAsync();

            var response = new TaskSearchResponse();

            response.AddRange(tasks);

            return response;
        }
    }
}
