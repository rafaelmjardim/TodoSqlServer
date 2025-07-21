using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TodoSqlServer.Services.Logic;
using TodoSqlServer.Repositories.interfaces;
using TodoSqlServer.DTOs;
using TodoSqlServer.Models;

namespace TodoSqlServer.Controllers
{
    [Route("api/tasks")]
    [ApiController]
    [Authorize]
    public class TodoController : ControllerBase
    {

        private readonly ITodoRepository _todoRepository;

        public TodoController(ITodoRepository taskRepository)
        {
            _todoRepository = taskRepository;
        }

        [HttpGet]
        public async Task<IResult> GetTodos()
        {
            var userId = TokenService.GetUserToken(User);
            var todoItems = await _todoRepository.GetTodos(userId);

            if (todoItems == null)
            {
                return Results.NotFound("Nenhum item encontrado");
            }
            
            return Results.Ok(todoItems);
        }

        [HttpPost]
        public async Task<IResult> PostTodoList([FromBody] TodoItemDto todoItemResponse)
        {
            Guid userId = TokenService.GetUserToken(User);

            var response = await _todoRepository.PostTodoItem(userId, todoItemResponse);

            if (response == null)
            {
                return Results.NotFound("Erro ao cadastrar tarefa");
            }

            await _todoRepository.CommitSaveChangesAsync();

            return Results.Ok(response);
        }

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
