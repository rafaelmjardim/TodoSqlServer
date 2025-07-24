using Microsoft.EntityFrameworkCore;
using TodoSqlServer.DTOs;
using TodoSqlServer.Models;
using TodoSqlServer.Repositories.interfaces;
using TodoSqlServer.Services.Interfaces;

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

        public async Task<TodoItem> PostTodoItem(Guid userId, TodoItemDto todoItemRequest)
        {
            var newTodo = new TodoItem
            {
                Title = todoItemRequest.Title,
                Description = todoItemRequest?.Description,
                UserId = userId
            };

            await _context.TodoItems.AddAsync(newTodo);

            return newTodo;
        }

        public async Task<bool> DeleteTodoItem(Guid userId, Guid itemId)
        {
            var todoItem = await _context.TodoItems.FirstAsync(i => i.UserId == userId && i.Id == itemId);

            if (todoItem == null) {
                return false;
            }

            _context.TodoItems.Remove(todoItem);

            return true;
        }

        public async Task CommitSaveChangesAsync()
        {
           await _context.SaveChangesAsync();
        }
    }
}
