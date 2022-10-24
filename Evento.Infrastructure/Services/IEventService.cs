using Evento.Evento.Core.Domain;
using Evento.Evento.Infrastructure.DTO;
using Microsoft.AspNetCore.Mvc.Diagnostics;

namespace Evento.Evento.Infrastructure.Services
{
    public interface IEventService
    {
        Task<EventDetailsDto> GetAsync(Guid id);
        Task<EventDetailsDto> GetAsync(string name);
        Task<IEnumerable<EventDto>> BrowseAsync(string? name = null); // zwróć kolekcję eventów według filtru wyszukiwania
        Task CreateAsync(Guid id, string name, string description, DateTime startDate, DateTime endDate);
        Task AddTicketsAsync(Guid eventId, int amount, decimal price);
        Task UpdateAsync(Guid id, string name, string description);
        Task DeleteAsync(Guid id);
    }
}
