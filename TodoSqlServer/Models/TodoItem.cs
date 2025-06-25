namespace TodoSqlServer.Models
{
    public class TodoItem
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string? Description { get; set; }
        public bool IsChecked { get; set; } = false;

        //Chave estrangeira
        public Guid UserId { get; set; }

    }
}
