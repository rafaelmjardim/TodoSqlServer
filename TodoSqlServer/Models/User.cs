namespace TodoSqlServer.Models
{
    public class User
    {
        public Guid Id { get; set; }
        public string Username { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;

        //Relacionamento: 1 usuário pode ter varias tarefas
        public ICollection<TodoItem> TodoItems { get; set; } = new List<TodoItem>();
    }
}
