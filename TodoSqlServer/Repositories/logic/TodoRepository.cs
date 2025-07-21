using System.Security.Claims;
using Microsoft.EntityFrameworkCore;
using TodoSqlServer.Models;
using TodoSqlServer.Repositories.interfaces;
using TodoSqlServer.Services.Logic;

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

    }
}
