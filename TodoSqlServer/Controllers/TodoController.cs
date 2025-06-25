using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TodoSqlServer.DTOs;
using TodoSqlServer.Models;

namespace TodoSqlServer.Controllers
{
    [Route("api/todos")]
    [ApiController]
    [Authorize]
    public class TodoController : ControllerBase
    {

        private readonly TodoListContext _todoListContext;

        public TodoController(TodoListContext todoListContext)
        {
            _todoListContext = todoListContext;
        }

        [HttpGet]
        public async Task<IResult> GetTodoList()
        {
            Guid userId = Services.TokenService.GetUserToken(User);

            var todoItems = await _todoListContext.TodoItems.Where(i => i.UserId == userId).ToListAsync();

            return Results.Ok(todoItems);
        }

        [HttpPost]
        public async Task<IResult> PostTodoList(TodoItemDto todoItemDto)
        {
            Guid userId = Services.TokenService.GetUserToken(User);

            var item = new TodoItem
            {
                Title = todoItemDto.Title,
                Description = todoItemDto.Description,
                IsChecked = todoItemDto.IsChecked,
                UserId = userId,
            };

            await _todoListContext.TodoItems.AddAsync(item);
            await _todoListContext.SaveChangesAsync();

            return Results.Ok(item);
        }

        [HttpDelete("{id:Guid}")]
        public async Task<IResult> DeleteItem(Guid id) {
            Guid userId = Services.TokenService.GetUserToken(User);

            var itemToDelete = _todoListContext.TodoItems.FirstOrDefaultAsync(i => i.Id == id && i.UserId == userId);

            if (itemToDelete == null)
            {
                return Results.NotFound("Item não encontrado ou não pertence ao usuário atual.");
            }

            _todoListContext.TodoItems.Remove(await itemToDelete);
            await _todoListContext.SaveChangesAsync();

            return Results.Ok("Item deletado com sucesso!");
        }
    }
}
