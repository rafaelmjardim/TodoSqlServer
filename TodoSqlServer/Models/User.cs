namespace TodoSqlServer.Models
{
    public class User
    {
        public Guid Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

        //Relacionamento: 1 usuário pode ter varias tarefas
        public ICollection<TodoItem> TodoItems { get; set; }
    }
}
