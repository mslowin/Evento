namespace Evento.Evento.Infrastructure.Services
{
    public interface IUserService
    {
        Task RegisterAsync(Guid userid, string email, string name, string password, string role = "user");
        Task LoginAsync(string email, string password);
    }
}
