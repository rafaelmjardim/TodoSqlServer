namespace TodoSqlServer.DTOs
{
    public class TodoItemDto
    {
        public string Title { get; set; } = string.Empty;
        public string? Description { get; set; } = string.Empty;
        public bool IsChecked { get; set; } = false;

    }
}
