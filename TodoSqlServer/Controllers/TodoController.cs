using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TodoSqlServer.Services.Logic;
using TodoSqlServer.Repositories.interfaces;

namespace TodoSqlServer.Controllers
{
    [Route("api/tasks")]
    [ApiController]
    [Authorize]
    public class TodoController : ControllerBase
    {

        private readonly ITodoRepository _taskRespository;

        public TodoController(ITodoRepository taskRepository)
        {
            _taskRespository = taskRepository;
        }

        [HttpGet]
        public async Task<IResult> GetTodos()
        {
            var userId = TokenService.GetUserToken(User);
            var todoItems = await _taskRespository.GetTodos(userId);

            if (todoItems == null)
            {
                return Results.NotFound("Nenhum item encontrado");
            }
            
            return Results.Ok(todoItems);
        }

        //[HttpPost]
        //public async Task<IResult> PostTodoList(TodoItemDto todoItemDto)
        //{
        //    Guid userId = TokenService.GetUserToken(User);

        //    var item = new TodoItem
        //    {
        //        Title = todoItemDto.Title,
        //        Description = todoItemDto.Description,
        //        IsChecked = todoItemDto.IsChecked,
        //        UserId = userId,
        //    };

        //    await _todoListContext.TodoItems.AddAsync(item);
        //    await _todoListContext.SaveChangesAsync();

        //    return Results.Ok(item);
        //}

        //[HttpDelete("{id:Guid}")]
        //public async Task<IResult> DeleteItem(Guid id) {
        //    Guid userId = TokenService.GetUserToken(User);

        //    var itemToDelete = _todoListContext.TodoItems.FirstOrDefaultAsync(i => i.Id == id && i.UserId == userId);

        //    if (itemToDelete == null)
        //    {
        //        return Results.NotFound("Item não encontrado ou não pertence ao usuário atual.");
        //    }

        //    _todoListContext.TodoItems.Remove(await itemToDelete);
        //    await _todoListContext.SaveChangesAsync();

        //    return Results.Ok("Item deletado com sucesso!");
        //}
        //[HttpPut("{id:Guid}")]
        //public async Task<IResult> UpdateItem(Guid id, TodoItem todoItem) { 
        //    Guid userId = TokenService.GetUserToken(User);

        //    var itemToUpdate = await _todoListContext.TodoItems.FirstOrDefaultAsync(i => i.Id == id && i.UserId == userId);

        //    if (itemToUpdate == null)
        //    {
        //        return Results.NotFound("Item não encontrado ou não pertence ao usuário atual.");
        //    }

        //    itemToUpdate.Title = todoItem.Title;
        //    itemToUpdate.Description = todoItem.Description;
        //    itemToUpdate.IsChecked = todoItem.IsChecked;

        //    await _todoListContext.SaveChangesAsync();

        //    return Results.Ok("Atualizado com sucesso");
        //}
    }
}
