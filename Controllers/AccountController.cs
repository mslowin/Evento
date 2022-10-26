using Evento.Evento.Infrastructure.Commands.Users;
using Evento.Evento.Infrastructure.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Evento.Controllers
{
    public class AccountController : ApiControllerBase
    {
        private IUserService _userService;
        private ITicketService _ticketService;

        public AccountController(IUserService userService, ITicketService ticket)
        {
            _userService = userService;
            _ticketService = ticket;
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Get()
            => Json(await _userService.GetAccountAsync(UserId));

        [HttpGet("tickets")]
        [Authorize]
        public async Task<IActionResult> GetTickets()
            => Json(await _ticketService.GetForUserAsync(UserId));

        [HttpPost("register")]
        public async Task<IActionResult> Post([FromBody]Register command)
        {
            await _userService.RegisterAsync(Guid.NewGuid(), command.Email, command.Name, command.Password, command.Role);

            return Created("/account", null);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Post([FromBody]Login command)
            => Json(await _userService.LoginAsync(command.Email, command.Password));
    }
}
