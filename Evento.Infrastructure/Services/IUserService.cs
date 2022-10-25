using Evento.Evento.Infrastructure.DTO;

namespace Evento.Evento.Infrastructure.Services
{
    public interface IUserService
    {
        Task<AccountDto> GetAccountAsync(Guid userId);
        Task RegisterAsync(Guid userid, string email, string name, string password, string role = "user");
        Task<TokenDto> LoginAsync(string email, string password);
    }
}
