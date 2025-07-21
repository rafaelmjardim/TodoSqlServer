using TodoSqlServer.Models;

namespace TodoSqlServer.Repositories.interfaces
{
    public interface ITodoRepository
    {
        public Task<List<TodoItem>> GetTodos(Guid userId);
    }
}
