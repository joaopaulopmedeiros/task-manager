namespace TaskManager.Api.Requests
{
    public abstract class PaginatedRequest
    {
        public int Size { get; set; }
        public int Page { get; set; }
    }

    public class TaskSearchRequest : PaginatedRequest
    {
        public string? Title { get; set; }

        public new string ToString() => $"size={Size}&page={Page}&title={Title}";
    }
}
