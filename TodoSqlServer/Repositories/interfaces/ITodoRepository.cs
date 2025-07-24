using Microsoft.AspNetCore.Mvc;
using TodoSqlServer.DTOs;
using TodoSqlServer.Models;

namespace TodoSqlServer.Repositories.interfaces
{
    public interface ITodoRepository
    {
        public Task CommitSaveChangesAsync();
        public Task<List<TodoItem>> GetTodos(Guid userId);
        public Task<TodoItem> PostTodoItem(Guid userId, TodoItemDto todoItemRequest);
        public Task<bool> DeleteTodoItem(Guid userId, Guid itemId);
    }
}
