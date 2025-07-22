using System.Security.Claims;
using TodoSqlServer.DTOs;
using TodoSqlServer.Models;
using TodoSqlServer.Repositories.interfaces;
using TodoSqlServer.Services.Interfaces;

namespace TodoSqlServer.Services.Logic
{
    public class TodoService : ITodoInterface
    {
        public readonly ITodoRepository _repository;
        public TodoService(ITodoRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<TodoItem>> GetTodoItem(ClaimsPrincipal principal)
        {
            var userId = TokenService.GetUserToken(principal);

            return await _repository.GetTodos(userId);        
        }

        public async Task<TodoItemDto> PostTodoItem(ClaimsPrincipal principal, TodoItemDto todoItemRequest) {
            var userId = TokenService.GetUserToken(principal);

            var todoItem = await _repository.PostTodoItem(userId, todoItemRequest);

            return new TodoItemDto
            {
                Title = todoItem.Title,
                Description = todoItem.Description
            };
        }
    }
}
