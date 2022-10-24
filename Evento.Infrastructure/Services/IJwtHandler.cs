using Evento.Evento.Infrastructure.DTO;

namespace Evento.Evento.Infrastructure.Services
{
    public interface IJwtHandler
    {
        JwtDto CreateToken(Guid userId, string role);
    }
}
