namespace TodoSqlServer.DTOs
{
    public class TodoItemDto
    {
        public string Title { get; set; }
        public string? Description { get; set; }
        public bool IsChecked { get; set; } = false;

    }
}
