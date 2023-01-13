namespace TaskManager.Infrastructure.Models
{
    public class Task
    {
        public Guid Id { get; set; }
        public string? Title { get; set; }
        public string? Author { get; set; }
        public TaskStatusEnum Status { get; set; }
        public DateTime CreatedAt { get; set; }
    }

    public enum TaskStatusEnum : int
    {
        Created = 1,
        Approved,
        Rejected,
    }
}
