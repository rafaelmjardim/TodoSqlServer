using System.Security.Claims;
using TodoSqlServer.DTOs;
using TodoSqlServer.Models;

namespace TodoSqlServer.Services.Interfaces
{
    public interface ITodoInterface
    {
        public Task<List<TodoItem>> GetTodoItem(ClaimsPrincipal principal);
        public Task<TodoItemDto> PostTodoItem(ClaimsPrincipal principal, TodoItemDto todoRequest);
        public Task<bool> DeleteTodoItem(ClaimsPrincipal principal, Guid itemId);
    }
}
