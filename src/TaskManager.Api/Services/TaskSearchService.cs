using Microsoft.EntityFrameworkCore;
using Serilog;
using StackExchange.Redis;
using System.Text.Json;
using TaskManager.Api.Requests;
using TaskManager.Api.Responses;
using TaskManager.Infrastructure;

namespace TaskManager.Api.Services
{
    public class TaskSearchService : ITaskSearchService
    {
        private readonly IDatabase _cache;
        private readonly TaskDbContext _context;

        public TaskSearchService(IDatabase cache, TaskDbContext context)
        {
            _cache = cache;
            _context = context;
        }

        public async Task<TaskSearchResponse?> SearchByAsync(TaskSearchRequest request)
        {
            var key = request.ToString();

            Log.Information($"Searching tasks. Input: {key}");

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
                
                if (tasks != null && tasks.Any())
                {
                    Log.Information("Serializing tasks from mysql and updating redis for 5 minutes");
                    content = JsonSerializer.Serialize(tasks);
                    await _cache.StringSetAsync(key, content, TimeSpan.FromMinutes(5));
                    response.AddRange(tasks);
                }

                return response;
            } else
            {
                Log.Information($"Deserializing tasks from redis. Content: {content}");
                response = JsonSerializer.Deserialize<TaskSearchResponse>(content);
                return response;
            }
        }
    }
}
