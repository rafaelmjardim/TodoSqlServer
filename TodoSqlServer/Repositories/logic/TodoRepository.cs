using Microsoft.EntityFrameworkCore;
using TodoSqlServer.DTOs;
using TodoSqlServer.Models;
using TodoSqlServer.Repositories.interfaces;

namespace TodoSqlServer.Repositories.logic
{
    public class TodoRepository : ITodoRepository
    {
        private readonly TodoListContext _context;

        public TodoRepository(TodoListContext context)
        {
            _context = context;
        }

        public async Task<List<TodoItem>> GetTodos(Guid userId)
        {
            var todoItems = await _context.TodoItems.Where(i => i.UserId == userId).ToListAsync();

            return todoItems;
        }

        public async Task<TodoItemDto> PostTodoItem(Guid userId, TodoItemDto todoItemRequest)
        {
            var newTodo = new TodoItem
            {
                Title = todoItemRequest.Title,
                Description = todoItemRequest?.Description,
                UserId = userId
            };

            await _context.TodoItems.AddAsync(newTodo);
            return new TodoItemDto
            {
                Title = newTodo.Title,
                Description = newTodo.Description,
            };
        }

        public async Task CommitSaveChangesAsync()
        {
           await _context.SaveChangesAsync();
        }
    }
}
