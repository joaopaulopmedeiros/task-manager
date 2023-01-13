namespace TaskManager.Api.Responses
{
    public class TaskSearchResponse : List<TaskSearchItem>
    {
    }

    public class TaskSearchItem
    {
        public Guid Id { get; set; }
        public string? Title { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
