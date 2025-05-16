using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TodoSqlServer.DTOs;
using TodoSqlServer.Models;

namespace TodoSqlServer.Controllers
{
    [Route("api/auth/")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly TodoListContext _todoListContext;
        public AuthController(TodoListContext todoListContext)
        {
            _todoListContext = todoListContext;
        }

        [Authorize]
        [HttpGet]
        public async Task<IResult> GetUsers()
        {
            var data = await _todoListContext.Users.ToListAsync();

            return Results.Ok(data);
        }

        [HttpPost("register")]
        public async Task<IResult> Register(RegisterDto registerDto)
        {

            var user = new User
            {
                Username = registerDto.Username,
                Password = registerDto.Password,
                Email = registerDto.Email,
            };

            await _todoListContext.Users.AddAsync(user);
            await _todoListContext.SaveChangesAsync();

            return Results.Ok(user);
        }
        [HttpPost("login")]
        public async Task<IResult> Login(LoginDto loginDto)
        {

            var user = await _todoListContext.Users.FirstOrDefaultAsync(u => u.Username == loginDto.Username);
            if (user == null || user.Password != loginDto.Password)
            {
                return Results.NotFound("Usuario invalido");
            }

            var token = Services.TokenService.GenerateToken(user);
            return Results.Ok(token);
        }
    }
}
