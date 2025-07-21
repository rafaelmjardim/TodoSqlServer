using Microsoft.AspNetCore.Mvc;
using TodoSqlServer.DTOs;
using TodoSqlServer.Models;

namespace TodoSqlServer.Repositories.interfaces
{
    public interface ITodoRepository
    {
        public Task CommitSaveChangesAsync();
        public Task<List<TodoItem>> GetTodos(Guid userId);
        public Task<TodoItemDto> PostTodoItem(Guid userId, [FromBody] TodoItemDto todoItemRequest);
    }
}
