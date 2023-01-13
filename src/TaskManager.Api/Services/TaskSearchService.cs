using Microsoft.EntityFrameworkCore;
using StackExchange.Redis;
using System.Text.Json;
using TaskManager.Api.Requests;
using TaskManager.Api.Responses;
using TaskManager.Infrastructure;

namespace TaskManager.Api.Services
{
    public class TaskSearchService
    {
        private readonly TaskDbContext _context;
        private readonly IDatabase _cache;

        public TaskSearchService(IDatabase cache, TaskDbContext context)
        {
            _cache = cache;
            _context = context;
        }

        public async Task<TaskSearchResponse?> SearchByAsync(TaskSearchRequest request)
        {
            var key = request.ToString();

            var cachedValue = await _cache.StringGetAsync(key);

            var content = cachedValue.ToString();

            var response = new TaskSearchResponse();

            if (string.IsNullOrEmpty(content))
            {
                var tasks = await _context
                   .Tasks
                   .Where(t => EF.Functions.Like(t.Title, $"%{request.Title}%"))
                   .Skip((request.Page - 1) * request.Size)
                   .Take(request.Size)
                   .ToListAsync();
                
                content = JsonSerializer.Serialize(tasks);

                await _cache.StringSetAsync(key, content, TimeSpan.FromMinutes(5));

                response.AddRange(tasks);

                return response;
            } else
            {
                response = JsonSerializer.Deserialize<TaskSearchResponse>(content);
                return response;
            }
        }
    }
}
