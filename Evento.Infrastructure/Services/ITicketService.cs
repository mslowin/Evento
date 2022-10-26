using Evento.Evento.Infrastructure.DTO;

namespace Evento.Evento.Infrastructure.Services
{
    public interface ITicketService
    {
        Task<TicketDto> GetAsync(Guid userId, Guid eventId, Guid ticketId);
        Task PurchaseAsync(Guid userId, Guid eventId, int amount);
        Task CancelAsync(Guid userId, Guid eventId, int amount);
    }
}
