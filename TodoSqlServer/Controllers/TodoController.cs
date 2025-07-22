using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TodoSqlServer.Services.Logic;
using TodoSqlServer.Repositories.interfaces;
using TodoSqlServer.DTOs;
using TodoSqlServer.Models;
using TodoSqlServer.Services.Interfaces;

namespace TodoSqlServer.Controllers
{
    [Route("api/tasks")]
    [ApiController]
    [Authorize]
    public class TodoController : ControllerBase
    {

        private readonly ITodoInterface _todoService;

        public TodoController(ITodoInterface todoService)
        {
            _todoService = todoService;
        }

        [HttpGet]
        public async Task<IResult> GetTodos()
        {
            var todoItems = await _todoService.GetTodoItem(User);

            if (todoItems.Count <= 0)
                return Results.NotFound("Nenhum item encontrado");

            return Results.Ok(todoItems);
        }

        [HttpPost]
        public async Task<IResult> PostTodoList([FromBody] TodoItemDto todoItemRequest)
        {
            var TodoItemDto = await _todoService.PostTodoItem(User, todoItemRequest);


            if (TodoItemDto == null)
            {
                return Results.NotFound("Erro ao cadastrar tarefa");
            }

            return Results.Ok(TodoItemDto);
        }

        //[HttpDelete("{id:Guid}")]
        //public async Task<IResult> DeleteItem()
        //{
        //    var userId = TokenService.GetUserToken(User);
        //    new_todoRepository.DeleteTodoItem(userId);
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
