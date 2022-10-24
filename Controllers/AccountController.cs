using Evento.Evento.Infrastructure.Commands.Users;
using Evento.Evento.Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;

namespace Evento.Controllers
{
    public class AccountController : Controller
    {
        private IUserService _userService;

        public AccountController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public Task<IActionResult> Get()
        {
            throw new NotImplementedException();
        }

        [HttpGet]
        public Task<IActionResult> GetTickets()
        {
            throw new NotImplementedException();
        }

        [HttpPost("register")]
        public async Task<IActionResult> Post(Register command)
        {
            await _userService.RegisterAsync(Guid.NewGuid(), command.Email, command.Name, command.Password, command.Role);

            return Created("/account", null);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Post(Login command)
        {
            throw new NotImplementedException();
        }
    }
}
